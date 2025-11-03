using BankTransferSimulator.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;

namespace BankTransferSimulator.Services
{
    public class Bank
    {
        public List<Account> Accounts { get; } = new List<Account>();
        private readonly Random _random = new Random();
        private bool _stop = false;

        public bool UseSafeMode { get; set; } = false;

        private readonly TextBox _log;
        private readonly object _logLock = new object();

        public Bank(int numAccounts, decimal initialBalance, TextBox log)
        {
            _log = log;
            for (int i = 1; i <= numAccounts; i++)
                Accounts.Add(new Account(i, initialBalance));
        }

        public void StartTransfers(int numThreads)
        {
            _stop = false;
            for (int i = 0; i < numThreads; i++)
            {
                int threadId = i + 1;
                var thread = new Thread(() => TransferLoop(threadId))
                {
                    IsBackground = true
                };
                thread.Start();
            }
        }

        public void Stop()
        {
            _stop = true;
        }

        public decimal TotalBalance()
        {
            decimal total = 0;
            foreach (var acc in Accounts)
                total += acc.Balance;
            return total;
        }

        private void TransferLoop(int threadId)
        {
            while (!_stop)
            {
                var src = Accounts[_random.Next(Accounts.Count)];
                var dst = Accounts[_random.Next(Accounts.Count)];
                if (src == dst) continue;

                decimal amount = _random.Next(1, 100);

                if (UseSafeMode)
                    SafeTransfer(src, dst, amount, threadId);
                else
                    UnsafeTransfer(src, dst, amount, threadId);

                Thread.Sleep(_random.Next(200, 800));
            }
        }

        // Unsafe transfer (can deadlock)
        private void UnsafeTransfer(Account src, Account dst, decimal amount, int threadId)
        {
            lock (src.GetLock())
            {
                Thread.Sleep(50); // increase chance of deadlock
                lock (dst.GetLock())
                {
                    if (src.Balance >= amount)
                    {
                        src.Withdraw(amount);
                        dst.Deposit(amount);
                        Log($"[UNSAFE][T{threadId}] {amount:C} {src.Id} → {dst.Id}");
                    }
                }
            }
        }

        // Safe transfer (lock ordering)
        private void SafeTransfer(Account src, Account dst, decimal amount, int threadId)
        {
            var first = src.Id < dst.Id ? src : dst;
            var second = src.Id < dst.Id ? dst : src;

            lock (first.GetLock())
            {
                Thread.Sleep(50);
                lock (second.GetLock())
                {
                    if (src.Balance >= amount)
                    {
                        src.Withdraw(amount);
                        dst.Deposit(amount);
                        Log($"[SAFE][T{threadId}] {amount:C} {src.Id} → {dst.Id}");
                    }
                }
            }
        }

        private void Log(string message)
        {
            if (_log.InvokeRequired)
            {
                _log.Invoke(new Action(() => AppendLog(message)));
            }
            else
            {
                AppendLog(message);
            }
        }

        private void AppendLog(string message)
        {
            lock (_logLock)
            {
                _log.AppendText(message + Environment.NewLine);
            }
        }
    }
}
