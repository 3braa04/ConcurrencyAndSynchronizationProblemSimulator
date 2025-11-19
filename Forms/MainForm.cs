using System;
using System.Drawing;
using System.Windows.Forms;
using BankTransferSimulator.Services;

namespace BankTransferSimulator.Forms
{
    public partial class MainForm : Form
    {
        private Bank _bank;
        private const decimal InitialAccountBalance = 1000m;
        private bool _isRunning = false;

        public MainForm()
        {
            InitializeComponent();
            InitializeUI();
        }

        private void InitializeUI()
        {
            lblTotalInitial.Text = "Expected Total: N/A";
            lblTotalCurrent.Text = "Current Total: N/A";
            lblStatus.Text = "Status: Ready to Start";
            lblStatus.ForeColor = Color.FromArgb(100, 100, 100);

            btnStop.Enabled = false;
            btnStart.Enabled = true;

            // إظهار الإحصائيات دائماً
            grpStatistics.Visible = true;

            // تعيين القيم الافتراضية للإحصائيات
            lblTransferCount.Text = "Completed: 0";
            lblFailedTransfers.Text = "Failed: 0";
            lblDeadlockCount.Text = "Deadlocks: 0";

            // Add tooltips
            var tooltip = new ToolTip();
            tooltip.SetToolTip(rbSafe, "Uses ordered lock acquisition to prevent deadlocks and ensure data consistency");
            tooltip.SetToolTip(rbUnsafe, "May cause deadlocks - threads will timeout after 5 seconds and continue");
            tooltip.SetToolTip(numAccounts, "Number of bank accounts in the simulation");
            tooltip.SetToolTip(numThreads, "Number of concurrent threads performing transfers");
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (_isRunning)
            {
                MessageBox.Show("Simulation is already running!", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Stop previous simulation if exists
            if (_bank != null)
            {
                _bank.Stop();
            }

            int accounts = (int)numAccounts.Value;
            int threads = (int)numThreads.Value;
            bool isSafe = rbSafe.Checked;

            // Clear log
            txtLog.Clear();

            // Initialize Bank
            _bank = new Bank(accounts, InitialAccountBalance, txtLog);
            _bank.UseSafeMode = isSafe;

            // Pass UI controls to Bank
            _bank.CurrentTotalLabel = lblTotalCurrent;
            _bank.StatusLabel = lblStatus;
            _bank.TransferCountLabel = lblTransferCount;
            _bank.FailedTransfersLabel = lblFailedTransfers;
            _bank.DeadlockCountLabel = lblDeadlockCount;

            // Set Initial UI Values
            decimal initialTotal = accounts * InitialAccountBalance;
            lblTotalInitial.Text = $"Expected Total: {initialTotal:C}";
            lblTotalCurrent.Text = $"Current Total: {initialTotal:C}";
            lblTotalCurrent.ForeColor = isSafe ? Color.Green : Color.OrangeRed;

            string modeText = isSafe ? "SAFE MODE ✓" : "UNSAFE MODE ⚠";
            lblStatus.Text = $"Status: Running - {modeText}";
            lblStatus.ForeColor = isSafe ? Color.FromArgb(0, 150, 0) : Color.FromArgb(220, 100, 0);
            lblStatus.BackColor = isSafe ? Color.FromArgb(230, 255, 230) : Color.FromArgb(255, 245, 230);

            // Log startup info
            string separator = new string('=', 80);
            txtLog.AppendText($"{separator}\r\n");
            txtLog.AppendText($"🚀 SIMULATION STARTED\r\n");
            txtLog.AppendText($"{separator}\r\n");
            txtLog.AppendText($"Configuration:\r\n");
            txtLog.AppendText($"  • Accounts: {accounts}\r\n");
            txtLog.AppendText($"  • Threads: {threads}\r\n");
            txtLog.AppendText($"  • Initial Balance per Account: {InitialAccountBalance:C}\r\n");
            txtLog.AppendText($"  • Total Expected Balance: {initialTotal:C}\r\n");
            txtLog.AppendText($"  • Mode: {(isSafe ? "SAFE (Ordered Lock Acquisition)" : "UNSAFE (Arbitrary Lock Order - Deadlocks Possible)")}\r\n");

            if (!isSafe)
            {
                txtLog.ForeColor = Color.Orange;
                txtLog.AppendText($"\r\n⚠ WARNING: Running in UNSAFE mode!\r\n");
                txtLog.AppendText($"  • Deadlocks may occur\r\n");
                txtLog.AppendText($"  • Threads will timeout after 5 seconds and continue\r\n");
                txtLog.AppendText($"  • Deadlock counter will track occurrences\r\n");
                txtLog.ForeColor = Color.LightGreen;
            }
            else
            {
                txtLog.AppendText($"\r\n✓ Running in SAFE mode\r\n");
                txtLog.AppendText($"  • Deadlock prevention via lock ordering\r\n");
                txtLog.AppendText($"  • Guaranteed data consistency\r\n");
            }

            txtLog.AppendText($"{separator}\r\n\r\n");

            // Update button states
            btnStart.Enabled = false;
            btnStop.Enabled = true;

            // Disable configuration changes during simulation
            numAccounts.Enabled = false;
            numThreads.Enabled = false;
            rbSafe.Enabled = false;
            rbUnsafe.Enabled = false;

            _isRunning = true;

            // Start Simulation
            _bank.StartTransfers(threads);
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if (_bank == null || !_isRunning)
            {
                return;
            }

            btnStop.Enabled = false;
            lblStatus.Text = "Status: Stopping...";
            lblStatus.ForeColor = Color.Orange;

            // Stop the bank operations
            _bank.Stop();

            // Final balance check
            decimal finalTotal = _bank.TotalBalance();
            decimal expectedTotal = (int)numAccounts.Value * InitialAccountBalance;
            long completedTransfers = _bank.GetTransferCount();
            long failedTransfers = _bank.GetFailedTransfers();
            long deadlockCount = _bank.GetDeadlockCount();

            string separator = new string('=', 80);
            txtLog.AppendText($"\r\n{separator}\r\n");
            txtLog.AppendText($"🛑 SIMULATION STOPPED\r\n");
            txtLog.AppendText($"{separator}\r\n");
            txtLog.AppendText($"Final Results:\r\n");
            //txtLog.AppendText($"  • Expected Total Balance: {expectedTotal:C}\r\n");
            //txtLog.AppendText($"  • Actual Total Balance: {finalTotal:C}\r\n");
            txtLog.AppendText($"  • Completed Transfers: {completedTransfers:N0}\r\n");
            txtLog.AppendText($"  • Failed Transfers: {failedTransfers:N0}\r\n");
            txtLog.AppendText($"  • Deadlocks Detected: {deadlockCount:N0}\r\n");

            //bool balanceConsistent = Math.Abs(finalTotal - expectedTotal) < 0.01m;

            //if (!balanceConsistent)
            //{
            //    txtLog.ForeColor = Color.Red;
            //    txtLog.AppendText($"\r\n❌ CRITICAL ERROR: Balance Inconsistent!\r\n");
            //    txtLog.AppendText($"  Difference: {(finalTotal - expectedTotal):C}\r\n");
            //    txtLog.AppendText($"  This indicates data corruption due to race conditions!\r\n");
            //    txtLog.ForeColor = Color.LightGreen;

            //    lblTotalCurrent.ForeColor = Color.Red;
            //    lblStatus.Text = "Status: Stopped - ❌ DATA CORRUPTED";
            //    lblStatus.ForeColor = Color.DarkRed;
            //    lblStatus.BackColor = Color.FromArgb(255, 230, 230);
            //}
            //else
            //{
            //    txtLog.ForeColor = Color.LightGreen;
            //    txtLog.AppendText($"\r\n✓ SUCCESS: Balance is Consistent!\r\n");
            //    txtLog.AppendText($"  All transfers completed without data corruption.\r\n");

            //    lblTotalCurrent.ForeColor = Color.Green;
            //    lblStatus.Text = "Status: Stopped - ✓ Data Consistent";
            //    lblStatus.ForeColor = Color.Green;
            //    lblStatus.BackColor = Color.FromArgb(230, 255, 230);
            //}

            lblTotalCurrent.ForeColor = Color.Green;
            lblStatus.Text = "Status: Stopped";
            lblStatus.ForeColor = Color.Green;
            lblStatus.BackColor = Color.FromArgb(230, 255, 230);

            txtLog.AppendText($"{separator}\r\n");

            // Update UI
            lblTotalCurrent.Text = $"Current Total: {finalTotal:C}";

            // Re-enable controls
            btnStart.Enabled = true;
            btnStop.Enabled = false;
            numAccounts.Enabled = true;
            numThreads.Enabled = true;
            rbSafe.Enabled = true;
            rbUnsafe.Enabled = true;

            _isRunning = false;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            if (!_isRunning)
            {
                txtLog.Clear();
            }
            else
            {
                var result = MessageBox.Show(
                    "Simulation is running. Are you sure you want to clear the log?",
                    "Confirm Clear",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    txtLog.Clear();
                }
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (_bank != null && _isRunning)
            {
                _bank.Stop();
            }
            base.OnFormClosing(e);
        }
    }
}