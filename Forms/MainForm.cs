using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BankTransferSimulator.Services;

namespace BankTransferSimulator.Forms
{
    public partial class MainForm : Form
    {
        private Bank _bank;

        public MainForm()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            int accounts = (int)numAccounts.Value;
            int threads = (int)numThreads.Value;

            _bank = new Bank(accounts, 1000m, txtLog);
            _bank.UseSafeMode = rbSafe.Checked;

            txtLog.AppendText($"Starting simulation in {(_bank.UseSafeMode ? "SAFE" : "UNSAFE")} mode...\r\n");
            _bank.StartTransfers(threads);
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if (_bank != null)
            {
                _bank.Stop();
                txtLog.AppendText($"Simulation stopped. Total balance = {_bank.TotalBalance():C}\r\n");
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtLog.Clear();
        }
    }
}
