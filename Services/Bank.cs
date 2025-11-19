using BankTransferSimulator.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;
using System.Linq;

namespace BankTransferSimulator.Services
{
    public class Bank
    {
        public List<Account> Accounts { get; } = new List<Account>();
        private readonly Random _random = new Random();
        private volatile bool _stop = false;
        private long _transferCount = 0;
        private long _failedTransfers = 0;
        private long _deadlockCount = 0;

        public bool UseSafeMode { get; set; } = false;

        private readonly TextBox _log;
        private readonly object _logLock = new object();
        private readonly object _balanceLock = new object();
        private const decimal InitialAccountBalance = 1000m;

        public Label CurrentTotalLabel { get; set; }
        public Label StatusLabel { get; set; }
        public Label TransferCountLabel { get; set; }
        public Label FailedTransfersLabel { get; set; }
        public Label DeadlockCountLabel { get; set; }

        private Thread _balanceCheckThread;
        private List<Thread> _transferThreads = new List<Thread>();

        public Bank(int numAccounts, decimal initialBalance, TextBox log)
        {
            _log = log;
            for (int i = 1; i <= numAccounts; i++)
                Accounts.Add(new Account(i, initialBalance));
        }

        public void StartTransfers(int numThreads)
        {
            _stop = false;
            _transferCount = 0;
            _failedTransfers = 0;
            _deadlockCount = 0;
            _transferThreads.Clear();

            for (int i = 0; i < numThreads; i++)
            {
                int threadId = i + 1;
                var thread = new Thread(() => TransferLoop(threadId))
                {
                    IsBackground = true,
                    Name = $"TransferThread-{threadId}"
                };
                thread.Start();
                _transferThreads.Add(thread);
            }

            _balanceCheckThread = new Thread(BalanceCheckLoop)
            {
                IsBackground = true,
                Name = "BalanceCheckThread"
            };
            _balanceCheckThread.Start();
        }

        public void Stop()
        {
            _stop = true;
            Thread.Sleep(100);
        }

        public decimal TotalBalance()
        {
            lock (_balanceLock)
            {
                return Accounts.Sum(acc => acc.Balance);
            }
        }

        public long GetTransferCount() => Interlocked.Read(ref _transferCount);
        public long GetFailedTransfers() => Interlocked.Read(ref _failedTransfers);
        public long GetDeadlockCount() => Interlocked.Read(ref _deadlockCount);

        private void BalanceCheckLoop()
        {
            decimal expectedTotal = Accounts.Count * InitialAccountBalance;

            while (!_stop)
            {
                Thread.Sleep(300);

                try
                {
                    if (CurrentTotalLabel != null && !CurrentTotalLabel.IsDisposed)
                    {
                        CurrentTotalLabel.BeginInvoke(new Action(() =>
                        {
                            try
                            {
                                decimal currentTotal = TotalBalance();
                                CurrentTotalLabel.Text = $"Current Total: {currentTotal:C}";

                                if (Math.Abs(currentTotal - expectedTotal) > 0.01m)
                                {
                                    CurrentTotalLabel.ForeColor = System.Drawing.Color.Red;
                                    StatusLabel?.BeginInvoke(new Action(() =>
                                    {
                                        StatusLabel.Text = "Status: ⚠ CRITICAL ERROR - Balance Corrupted!";
                                        StatusLabel.ForeColor = System.Drawing.Color.DarkRed;
                                    }));
                                }
                                else if (!UseSafeMode)
                                {
                                    CurrentTotalLabel.ForeColor = System.Drawing.Color.OrangeRed;
                                }
                                else
                                {
                                    CurrentTotalLabel.ForeColor = System.Drawing.Color.Green;
                                }
                            }
                            catch { }
                        }));
                    }

                    if (TransferCountLabel != null && !TransferCountLabel.IsDisposed)
                    {
                        TransferCountLabel.BeginInvoke(new Action(() =>
                        {
                            try
                            {
                                TransferCountLabel.Text = $"Completed: {GetTransferCount():N0}";
                            }
                            catch { }
                        }));
                    }

                    if (FailedTransfersLabel != null && !FailedTransfersLabel.IsDisposed)
                    {
                        FailedTransfersLabel.BeginInvoke(new Action(() =>
                        {
                            try
                            {
                                FailedTransfersLabel.Text = $"Failed: {GetFailedTransfers():N0}";
                            }
                            catch { }
                        }));
                    }

                    if (DeadlockCountLabel != null && !DeadlockCountLabel.IsDisposed)
                    {
                        DeadlockCountLabel.BeginInvoke(new Action(() =>
                        {
                            try
                            {
                                DeadlockCountLabel.Text = $"Deadlocks: {GetDeadlockCount():N0}";
                            }
                            catch { }
                        }));
                    }
                }
                catch (Exception)
                {
                    break;
                }
            }
        }

        private void TransferLoop(int threadId)
        {
            while (!_stop)
            {
                try
                {
                    var srcIndex = _random.Next(Accounts.Count);
                    var dstIndex = _random.Next(Accounts.Count);

                    if (srcIndex == dstIndex) continue;

                    var src = Accounts[srcIndex];
                    var dst = Accounts[dstIndex];

                    decimal amount = _random.Next(1, 100);

                    bool success;
                    if (UseSafeMode)
                        success = SafeTransfer(src, dst, amount, threadId);
                    else
                        success = UnsafeTransfer(src, dst, amount, threadId);

                    if (success)
                        Interlocked.Increment(ref _transferCount);
                    else
                        Interlocked.Increment(ref _failedTransfers);

                    Thread.Sleep(_random.Next(10, 100));
                }
                catch (Exception ex)
                {
                    Log($"[ERROR][T{threadId}] Exception: {ex.Message}");
                }
            }
        }

        private bool UnsafeTransfer(Account src, Account dst, decimal amount, int threadId)
        {
            // ولكن مع timeout لمنع تجمد البرنامج
            if (Monitor.TryEnter(src.GetLock(), 5000)) // 5 ثواني timeout
            {
                try
                {
                    Thread.Sleep(10); // زيادة فرصة حدوث deadlock

                    if (Monitor.TryEnter(dst.GetLock(), 5000))
                    {
                        try
                        {
                            if (src.Balance >= amount)
                            {
                                src.Withdraw(amount);
                                dst.Deposit(amount);
                                Log($"[UNSAFE][T{threadId}] {amount:C} | Acc#{src.Id} → Acc#{dst.Id}");
                                return true;
                            }
                            return false;
                        }
                        finally
                        {
                            Monitor.Exit(dst.GetLock());
                        }
                    }
                    else
                    {
                        // DEADLOCK DETECTED!
                        Interlocked.Increment(ref _deadlockCount);
                        Log($"[DEADLOCK][T{threadId}] ⚠ Could not acquire second lock! Transfer failed: {amount:C} | Acc#{src.Id} → Acc#{dst.Id}");
                        return false;
                    }
                }
                finally
                {
                    Monitor.Exit(src.GetLock());
                }
            }
            else
            {
                // DEADLOCK DETECTED!
                Interlocked.Increment(ref _deadlockCount);
                Log($"[DEADLOCK][T{threadId}] ⚠ Could not acquire first lock! Transfer failed: {amount:C} | Acc#{src.Id} → Acc#{dst.Id}");
                return false;
            }
        }

        private bool SafeTransfer(Account src, Account dst, decimal amount, int threadId)
        {
            var first = src.Id < dst.Id ? src : dst;
            var second = src.Id < dst.Id ? dst : src;

            lock (first.GetLock())
            {
                Thread.Sleep(5);
                lock (second.GetLock())
                {
                    if (src.Balance >= amount)
                    {
                        src.Withdraw(amount);
                        dst.Deposit(amount);
                        Log($"[SAFE][T{threadId}] {amount:C} | Acc#{src.Id} → Acc#{dst.Id}");
                        return true;
                    }
                    return false;
                }
            }
        }

        private void Log(string message)
        {
            try
            {
                if (_log.IsDisposed) return;

                if (_log.InvokeRequired)
                {
                    _log.BeginInvoke(new Action(() => AppendLog(message)));
                }
                else
                {
                    AppendLog(message);
                }
            }
            catch (Exception){}
        }

        private void AppendLog(string message)
        {
            try
            {
                lock (_logLock)
                {
                    if (_log.IsDisposed) return;

                    // Prevent memory issues by limiting log size
                    if (_log.Lines.Length > 1000)
                    {
                        var lines = _log.Lines;
                        _log.Lines = lines.Skip(500).ToArray();
                    }

                    _log.AppendText($"[{DateTime.Now:HH:mm:ss.fff}] {message}{Environment.NewLine}");
                }
            }
            catch (Exception){}
        }
    }
}