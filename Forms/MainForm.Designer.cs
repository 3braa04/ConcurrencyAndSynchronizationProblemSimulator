namespace BankTransferSimulator.Forms
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label lblAccounts;
        private System.Windows.Forms.NumericUpDown numAccounts;
        private System.Windows.Forms.Label lblThreads;
        private System.Windows.Forms.NumericUpDown numThreads;
        private System.Windows.Forms.RadioButton rbSafe;
        private System.Windows.Forms.RadioButton rbUnsafe;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblTotalInitial;
        private System.Windows.Forms.Label lblTotalCurrent;
        private System.Windows.Forms.Panel panelTotalBalance;
        private System.Windows.Forms.Label lblTransferCount;
        private System.Windows.Forms.Label lblFailedTransfers;
        private System.Windows.Forms.Label lblDeadlockCount;
        private System.Windows.Forms.GroupBox grpStatistics;
        private System.Windows.Forms.GroupBox grpSettings;
        private System.Windows.Forms.GroupBox grpMode;
        private System.Windows.Forms.Label lblLogHeader;
        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panelRight;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            lblAccounts = new Label();
            numAccounts = new NumericUpDown();
            lblThreads = new Label();
            numThreads = new NumericUpDown();
            rbSafe = new RadioButton();
            rbUnsafe = new RadioButton();
            btnStart = new Button();
            btnStop = new Button();
            btnClear = new Button();
            txtLog = new TextBox();
            label1 = new Label();
            label2 = new Label();
            panel1 = new Panel();
            panelRight = new Panel();
            grpStatistics = new GroupBox();
            lblDeadlockCount = new Label();
            lblFailedTransfers = new Label();
            lblTransferCount = new Label();
            grpMode = new GroupBox();
            grpSettings = new GroupBox();
            panel2 = new Panel();
            lblLogHeader = new Label();
            lblStatus = new Label();
            panelTotalBalance = new Panel();
            lblTotalCurrent = new Label();
            lblTotalInitial = new Label();
            panelHeader = new Panel();
            ((System.ComponentModel.ISupportInitialize)numAccounts).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numThreads).BeginInit();
            panel1.SuspendLayout();
            panelRight.SuspendLayout();
            grpStatistics.SuspendLayout();
            grpMode.SuspendLayout();
            grpSettings.SuspendLayout();
            panel2.SuspendLayout();
            panelTotalBalance.SuspendLayout();
            panelHeader.SuspendLayout();
            SuspendLayout();
            // 
            // lblAccounts
            // 
            lblAccounts.AutoSize = true;
            lblAccounts.Font = new Font("Segoe UI", 9.75F);
            lblAccounts.Location = new Point(15, 30);
            lblAccounts.Name = "lblAccounts";
            lblAccounts.Size = new Size(131, 17);
            lblAccounts.TabIndex = 0;
            lblAccounts.Text = "Number of Accounts:";
            // 
            // numAccounts
            // 
            numAccounts.Font = new Font("Segoe UI", 10F);
            numAccounts.Location = new Point(169, 28);
            numAccounts.Maximum = new decimal(new int[] { 50, 0, 0, 0 });
            numAccounts.Minimum = new decimal(new int[] { 2, 0, 0, 0 });
            numAccounts.Name = "numAccounts";
            numAccounts.Size = new Size(80, 25);
            numAccounts.TabIndex = 1;
            numAccounts.Value = new decimal(new int[] { 10, 0, 0, 0 });
            // 
            // lblThreads
            // 
            lblThreads.AutoSize = true;
            lblThreads.Font = new Font("Segoe UI", 9.75F);
            lblThreads.Location = new Point(15, 65);
            lblThreads.Name = "lblThreads";
            lblThreads.Size = new Size(126, 17);
            lblThreads.TabIndex = 2;
            lblThreads.Text = "Number of Threads:";
            // 
            // numThreads
            // 
            numThreads.Font = new Font("Segoe UI", 10F);
            numThreads.Location = new Point(169, 63);
            numThreads.Maximum = new decimal(new int[] { 50, 0, 0, 0 });
            numThreads.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numThreads.Name = "numThreads";
            numThreads.Size = new Size(80, 25);
            numThreads.TabIndex = 3;
            numThreads.Value = new decimal(new int[] { 10, 0, 0, 0 });
            // 
            // rbSafe
            // 
            rbSafe.AutoSize = true;
            rbSafe.Checked = true;
            rbSafe.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            rbSafe.ForeColor = Color.FromArgb(0, 150, 0);
            rbSafe.Location = new Point(15, 30);
            rbSafe.Name = "rbSafe";
            rbSafe.Size = new Size(233, 24);
            rbSafe.TabIndex = 4;
            rbSafe.TabStop = true;
            rbSafe.Text = "✓ Safe Mode (Ordered Locks)";
            // 
            // rbUnsafe
            // 
            rbUnsafe.AutoSize = true;
            rbUnsafe.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            rbUnsafe.ForeColor = Color.FromArgb(220, 50, 50);
            rbUnsafe.Location = new Point(15, 60);
            rbUnsafe.Name = "rbUnsafe";
            rbUnsafe.Size = new Size(274, 24);
            rbUnsafe.TabIndex = 5;
            rbUnsafe.Text = "⚠ Unsafe Mode (Race Conditions)";
            // 
            // btnStart
            // 
            btnStart.BackColor = Color.FromArgb(70, 200, 120);
            btnStart.FlatStyle = FlatStyle.Flat;
            btnStart.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnStart.ForeColor = Color.White;
            btnStart.Location = new Point(20, 20);
            btnStart.Name = "btnStart";
            btnStart.Size = new Size(170, 40);
            btnStart.TabIndex = 6;
            btnStart.Text = "▶ Start Simulation";
            btnStart.UseVisualStyleBackColor = false;
            btnStart.Click += btnStart_Click;
            // 
            // btnStop
            // 
            btnStop.BackColor = Color.FromArgb(220, 70, 70);
            btnStop.FlatStyle = FlatStyle.Flat;
            btnStop.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnStop.ForeColor = Color.White;
            btnStop.Location = new Point(20, 70);
            btnStop.Name = "btnStop";
            btnStop.Size = new Size(170, 40);
            btnStop.TabIndex = 7;
            btnStop.Text = "■ Stop Simulation";
            btnStop.UseVisualStyleBackColor = false;
            btnStop.Click += btnStop_Click;
            // 
            // btnClear
            // 
            btnClear.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnClear.BackColor = Color.FromArgb(100, 100, 100);
            btnClear.FlatStyle = FlatStyle.Flat;
            btnClear.Font = new Font("Segoe UI", 9F);
            btnClear.ForeColor = Color.White;
            btnClear.Location = new Point(974, 5);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(100, 30);
            btnClear.TabIndex = 8;
            btnClear.Text = "Clear Log";
            btnClear.UseVisualStyleBackColor = false;
            btnClear.Click += btnClear_Click;
            // 
            // txtLog
            // 
            txtLog.BackColor = Color.FromArgb(30, 30, 30);
            txtLog.Dock = DockStyle.Fill;
            txtLog.Font = new Font("Consolas", 9F);
            txtLog.ForeColor = Color.LightGreen;
            txtLog.Location = new Point(10, 35);
            txtLog.Multiline = true;
            txtLog.Name = "txtLog";
            txtLog.ReadOnly = true;
            txtLog.ScrollBars = ScrollBars.Vertical;
            txtLog.Size = new Size(1064, 247);
            txtLog.TabIndex = 9;
            // 
            // label1
            // 
            label1.Dock = DockStyle.Top;
            label1.Font = new Font("Segoe UI", 26F, FontStyle.Bold);
            label1.ForeColor = Color.FromArgb(40, 120, 220);
            label1.Location = new Point(0, 0);
            label1.Name = "label1";
            label1.Size = new Size(1084, 65);
            label1.TabIndex = 10;
            label1.Text = "Concurrency And Synchronization Simulator";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            label2.Dock = DockStyle.Top;
            label2.Font = new Font("Segoe UI", 10F);
            label2.ForeColor = Color.FromArgb(60, 60, 60);
            label2.Location = new Point(0, 65);
            label2.Name = "label2";
            label2.Padding = new Padding(15, 0, 15, 0);
            label2.Size = new Size(1084, 50);
            label2.TabIndex = 11;
            label2.Text = "Demonstrates how concurrent money transfers can cause data corruption (Unsafe Mode)\r\nor how proper lock ordering prevents deadlocks and ensures data integrity (Safe Mode).";
            label2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(245, 245, 245);
            panel1.Controls.Add(panelRight);
            panel1.Controls.Add(grpMode);
            panel1.Controls.Add(grpStatistics);
            panel1.Controls.Add(grpSettings);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 200);
            panel1.Name = "panel1";
            panel1.Padding = new Padding(10);
            panel1.Size = new Size(1084, 158);
            panel1.TabIndex = 12;
            // 
            // panelRight
            // 
            panelRight.Controls.Add(btnStart);
            panelRight.Controls.Add(btnStop);
            panelRight.Location = new Point(817, 15);
            panelRight.Name = "panelRight";
            panelRight.Size = new Size(212, 130);
            panelRight.TabIndex = 10;
            // 
            // grpStatistics
            // 
            grpStatistics.Controls.Add(lblDeadlockCount);
            grpStatistics.Controls.Add(lblFailedTransfers);
            grpStatistics.Controls.Add(lblTransferCount);
            grpStatistics.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            grpStatistics.Location = new Point(630, 15);
            grpStatistics.Name = "grpStatistics";
            grpStatistics.Size = new Size(170, 130);
            grpStatistics.TabIndex = 9;
            grpStatistics.TabStop = false;
            grpStatistics.Text = "Statistics";
            // 
            // lblDeadlockCount
            // 
            lblDeadlockCount.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblDeadlockCount.ForeColor = Color.FromArgb(180, 80, 180);
            lblDeadlockCount.Location = new Point(10, 90);
            lblDeadlockCount.Name = "lblDeadlockCount";
            lblDeadlockCount.Size = new Size(150, 25);
            lblDeadlockCount.TabIndex = 2;
            lblDeadlockCount.Text = "Deadlocks: 0";
            lblDeadlockCount.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblFailedTransfers
            // 
            lblFailedTransfers.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblFailedTransfers.ForeColor = Color.FromArgb(200, 80, 80);
            lblFailedTransfers.Location = new Point(10, 60);
            lblFailedTransfers.Name = "lblFailedTransfers";
            lblFailedTransfers.Size = new Size(150, 25);
            lblFailedTransfers.TabIndex = 1;
            lblFailedTransfers.Text = "Failed: 0";
            lblFailedTransfers.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblTransferCount
            // 
            lblTransferCount.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblTransferCount.ForeColor = Color.FromArgb(0, 120, 215);
            lblTransferCount.Location = new Point(10, 30);
            lblTransferCount.Name = "lblTransferCount";
            lblTransferCount.Size = new Size(150, 25);
            lblTransferCount.TabIndex = 0;
            lblTransferCount.Text = "Completed: 0";
            lblTransferCount.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // grpMode
            // 
            grpMode.Controls.Add(rbSafe);
            grpMode.Controls.Add(rbUnsafe);
            grpMode.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            grpMode.Location = new Point(300, 15);
            grpMode.Name = "grpMode";
            grpMode.Size = new Size(310, 130);
            grpMode.TabIndex = 8;
            grpMode.TabStop = false;
            grpMode.Text = "Execution Mode";
            // 
            // grpSettings
            // 
            grpSettings.Controls.Add(lblAccounts);
            grpSettings.Controls.Add(numAccounts);
            grpSettings.Controls.Add(lblThreads);
            grpSettings.Controls.Add(numThreads);
            grpSettings.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            grpSettings.Location = new Point(20, 15);
            grpSettings.Name = "grpSettings";
            grpSettings.Size = new Size(263, 130);
            grpSettings.TabIndex = 7;
            grpSettings.TabStop = false;
            grpSettings.Text = "Configuration";
            // 
            // panel2
            // 
            panel2.BackColor = Color.White;
            panel2.Controls.Add(btnClear);
            panel2.Controls.Add(txtLog);
            panel2.Controls.Add(lblLogHeader);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(0, 358);
            panel2.Name = "panel2";
            panel2.Padding = new Padding(10);
            panel2.Size = new Size(1084, 292);
            panel2.TabIndex = 13;
            // 
            // lblLogHeader
            // 
            lblLogHeader.Dock = DockStyle.Top;
            lblLogHeader.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblLogHeader.ForeColor = Color.FromArgb(60, 60, 60);
            lblLogHeader.Location = new Point(10, 10);
            lblLogHeader.Name = "lblLogHeader";
            lblLogHeader.Size = new Size(1064, 25);
            lblLogHeader.TabIndex = 10;
            lblLogHeader.Text = "📋 Transaction Log";
            lblLogHeader.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblStatus
            // 
            lblStatus.BackColor = Color.FromArgb(240, 240, 240);
            lblStatus.Dock = DockStyle.Top;
            lblStatus.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            lblStatus.ForeColor = Color.FromArgb(100, 100, 100);
            lblStatus.Location = new Point(0, 115);
            lblStatus.Name = "lblStatus";
            lblStatus.Padding = new Padding(0, 5, 0, 5);
            lblStatus.Size = new Size(1084, 35);
            lblStatus.TabIndex = 14;
            lblStatus.Text = "Status: Ready to Start";
            lblStatus.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panelTotalBalance
            // 
            panelTotalBalance.BackColor = Color.FromArgb(250, 250, 250);
            panelTotalBalance.BorderStyle = BorderStyle.FixedSingle;
            panelTotalBalance.Controls.Add(lblTotalCurrent);
            panelTotalBalance.Controls.Add(lblTotalInitial);
            panelTotalBalance.Dock = DockStyle.Top;
            panelTotalBalance.Location = new Point(0, 150);
            panelTotalBalance.Name = "panelTotalBalance";
            panelTotalBalance.Size = new Size(1084, 50);
            panelTotalBalance.TabIndex = 15;
            // 
            // lblTotalCurrent
            // 
            lblTotalCurrent.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTotalCurrent.ForeColor = Color.FromArgb(50, 50, 50);
            lblTotalCurrent.Location = new Point(410, 8);
            lblTotalCurrent.Name = "lblTotalCurrent";
            lblTotalCurrent.Size = new Size(400, 32);
            lblTotalCurrent.TabIndex = 1;
            lblTotalCurrent.Text = "Current Total: N/A";
            lblTotalCurrent.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblTotalInitial
            // 
            lblTotalInitial.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTotalInitial.ForeColor = Color.FromArgb(50, 50, 50);
            lblTotalInitial.Location = new Point(10, 8);
            lblTotalInitial.Name = "lblTotalInitial";
            lblTotalInitial.Size = new Size(395, 32);
            lblTotalInitial.TabIndex = 0;
            lblTotalInitial.Text = "Expected Total: N/A";
            lblTotalInitial.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panelHeader
            // 
            panelHeader.Controls.Add(label2);
            panelHeader.Controls.Add(label1);
            panelHeader.Dock = DockStyle.Top;
            panelHeader.Location = new Point(0, 0);
            panelHeader.Name = "panelHeader";
            panelHeader.Size = new Size(1084, 115);
            panelHeader.TabIndex = 16;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1084, 650);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Controls.Add(panelTotalBalance);
            Controls.Add(lblStatus);
            Controls.Add(panelHeader);
            MinimumSize = new Size(836, 689);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Concurrency Simulator - Bank Transfers";
            ((System.ComponentModel.ISupportInitialize)numAccounts).EndInit();
            ((System.ComponentModel.ISupportInitialize)numThreads).EndInit();
            panel1.ResumeLayout(false);
            panelRight.ResumeLayout(false);
            grpStatistics.ResumeLayout(false);
            grpMode.ResumeLayout(false);
            grpMode.PerformLayout();
            grpSettings.ResumeLayout(false);
            grpSettings.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panelTotalBalance.ResumeLayout(false);
            panelHeader.ResumeLayout(false);
            ResumeLayout(false);
        }
    }
}