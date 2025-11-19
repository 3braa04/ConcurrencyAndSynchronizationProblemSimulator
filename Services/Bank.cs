using BankTransferSimulator.Models;

namespace BankTransferSimulator.Services
{
    public class Bank
    {
        public List<Account> Accounts { get; } = new List<Account>();
        private readonly Random _random = new Random();
        private bool _stop = false;
        private long _transferCount = 0;
        private long _failedTransfers = 0;
        private long _deadlockCount = 0;
        private long _raceConditionCount = 0;

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

        private Thread _StatisticsCheck;
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
            _raceConditionCount = 0;
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

            _StatisticsCheck = new Thread(StatisticsCheckLoop)
            {
                IsBackground = true,
                Name = "StatisticsCheck"
            };
            _StatisticsCheck.Start();
        }

        private void StatisticsCheckLoop()
        {
            decimal expectedTotal = Accounts.Count * InitialAccountBalance;
            decimal difference = 0;

            while (!_stop)
            {
                Thread.Sleep(100);

                try
                {
                    if (CurrentTotalLabel != null && !CurrentTotalLabel.IsDisposed)
                    {
                        CurrentTotalLabel.BeginInvoke(new Action(() =>
                        {
                            decimal currentTotal = TotalBalance();
                            difference += Math.Abs(currentTotal - expectedTotal);
                            CurrentTotalLabel.Text = $"Current Total: {expectedTotal - difference:C}";

                            if (difference > 0.01m)
                            {
                                Interlocked.Increment(ref _raceConditionCount);
                                CurrentTotalLabel.ForeColor = Color.Red;
                                StatusLabel?.BeginInvoke(new Action(() =>
                                {
                                    StatusLabel.Text = $"Status: ⚠ CRITICAL ERROR - Balance Corrupted by {difference:C}!";
                                    StatusLabel.ForeColor = Color.DarkRed;
                                }));
                            }

                            CurrentTotalLabel.ForeColor = UseSafeMode ? Color.Green : Color.OrangeRed;
                        }));
                    }

                    if (TransferCountLabel != null && !TransferCountLabel.IsDisposed)
                    {
                        TransferCountLabel.BeginInvoke(new Action(() =>
                        {
                            TransferCountLabel.Text = $"Completed: {GetTransferCount():N0}";
                        }));
                    }

                    if (FailedTransfersLabel != null && !FailedTransfersLabel.IsDisposed)
                    {
                        FailedTransfersLabel.BeginInvoke(new Action(() =>
                        {
                            FailedTransfersLabel.Text = $"Failed: {GetFailedTransfers():N0}";
                        }));
                    }

                    if (DeadlockCountLabel != null && !DeadlockCountLabel.IsDisposed)
                    {
                        DeadlockCountLabel.BeginInvoke(new Action(() =>
                        {
                            DeadlockCountLabel.Text = $"Deadlocks: {GetDeadlockCount():N0}";
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

                    Thread.Sleep(_random.Next(5, 50));
                }
                catch (Exception ex)
                {
                    Log($"[ERROR][T{threadId}] Exception: {ex.Message}");
                }
            }
        }

        private bool UnsafeTransfer(Account src, Account dst, decimal amount, int threadId)
        {
            bool srcLocked = false;
            bool dstLocked = false;

            try
            {
                if (!Monitor.TryEnter(src.GetLock(), 1000))
                {
                    Interlocked.Increment(ref _deadlockCount);
                    Log($"[DEADLOCK][T{threadId}] ⚠ Timeout on first lock! Acc#{src.Id} → Acc#{dst.Id} ({amount:C})");
                    return false;
                }
                srcLocked = true;


                if (!Monitor.TryEnter(dst.GetLock(), 1000))
                {
                    Interlocked.Increment(ref _deadlockCount);
                    Log($"[DEADLOCK][T{threadId}] ⚠ Timeout on second lock! Acc#{src.Id} → Acc#{dst.Id} ({amount:C})");
                    return false;
                }
                dstLocked = true;

                if (src.Balance >= amount)
                {
                    src.Withdraw(amount);
                    Thread.Sleep(5);
                    dst.Deposit(amount);

                    Log($"[UNSAFE][T{threadId}] {amount:C} | Acc#{src.Id} → Acc#{dst.Id}");
                    return true;
                }
                else
                {
                    Log($"FAILED");
                    return false;
                }
            }
            finally
            {
                if (dstLocked)
                    try { Monitor.Exit(dst.GetLock()); } catch { }
                
                if (srcLocked)
                    try { Monitor.Exit(src.GetLock()); } catch { }
            }
        }

        private bool SafeTransfer(Account src, Account dst, decimal amount, int threadId)
        {
            bool firstLocked = false;
            bool secondLocked = false;
            var first = src.Id < dst.Id ? src : dst;
            var second = src.Id < dst.Id ? dst : src;

            try
            {
                if (!Monitor.TryEnter(first.GetLock(), 1000))
                {
                    Interlocked.Increment(ref _deadlockCount);
                    Log($"[DEADLOCK][T{threadId}] ⚠ Timeout on first lock! Acc#{src.Id} → Acc#{dst.Id} ({amount:C})");
                    return false;
                }
                firstLocked = true;

                if (!Monitor.TryEnter(second.GetLock(), 1000))
                {
                    Interlocked.Increment(ref _deadlockCount);
                    Log($"[DEADLOCK][T{threadId}] ⚠ Timeout on second lock! Acc#{src.Id} → Acc#{dst.Id} ({amount:C})");
                    return false;
                }
                secondLocked = true;

                if (src.Balance >= amount)
                {
                    src.Withdraw(amount);
                    dst.Deposit(amount);

                    Log($"[SAFE][T{threadId}] {amount:C} | Acc#{src.Id} → Acc#{dst.Id}");
                    return true;
                }
                else
                {
                    Log($"FAILED");
                    return false;
                }
            }
            finally
            {
                if (firstLocked)
                    try { Monitor.Exit(first.GetLock()); } catch { }

                if (secondLocked)
                    try { Monitor.Exit(second.GetLock()); } catch { }
            }
        }



        public void Stop()
        {
            _stop = true;

            if (_StatisticsCheck != null && _StatisticsCheck.IsAlive)
            {
                _StatisticsCheck.Join(2000);
            }

            foreach (var thread in _transferThreads)
            {
                if (thread != null && thread.IsAlive)
                {
                    thread.Join(1000);
                }
            }
        }

        public decimal TotalBalance()
        {
            if (!UseSafeMode)
            {
                return Accounts.Sum(acc => acc.Balance);
            }
            else
            {
                lock (_balanceLock)
                {
                    return Accounts.Sum(acc => acc.Balance);
                }
            }
        }

        public long GetTransferCount() => Interlocked.Read(ref _transferCount);
        public long GetFailedTransfers() => Interlocked.Read(ref _failedTransfers);
        public long GetDeadlockCount() => Interlocked.Read(ref _deadlockCount);
        public long GetRaceConditionCount() => Interlocked.Read(ref _raceConditionCount);

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
            catch (Exception) { }
        }

        private void AppendLog(string message)
        {
            try
            {
                lock (_logLock)
                {
                    if (_log.IsDisposed) return;

                    // Prevent memory issues
                    if (_log.Lines.Length > 1000)
                    {
                        var lines = _log.Lines;
                        _log.Lines = lines.Skip(500).ToArray();
                    }

                    _log.AppendText($"[{DateTime.Now:HH:mm:ss.fff}] {message}{Environment.NewLine}");
                }
            }
            catch (Exception)
            {
                // Ignore logging errors
            }
        }
    }
}