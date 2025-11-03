namespace BankTransferSimulator.Forms
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        // ------------------------------
        // Add these class-level control declarations
        // ------------------------------
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

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support
        /// </summary>
        private void InitializeComponent()
        {
            this.lblAccounts = new System.Windows.Forms.Label();
            this.numAccounts = new System.Windows.Forms.NumericUpDown();
            this.lblThreads = new System.Windows.Forms.Label();
            this.numThreads = new System.Windows.Forms.NumericUpDown();
            this.rbSafe = new System.Windows.Forms.RadioButton();
            this.rbUnsafe = new System.Windows.Forms.RadioButton();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.txtLog = new System.Windows.Forms.TextBox();

            ((System.ComponentModel.ISupportInitialize)(this.numAccounts)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numThreads)).BeginInit();
            this.SuspendLayout();

            // lblAccounts
            this.lblAccounts.AutoSize = true;
            this.lblAccounts.Location = new System.Drawing.Point(30, 30);
            this.lblAccounts.Text = "Accounts:";

            // numAccounts
            this.numAccounts.Location = new System.Drawing.Point(120, 28);
            this.numAccounts.Minimum = 1;
            this.numAccounts.Maximum = 50;
            this.numAccounts.Value = 5;
            this.numAccounts.Size = new System.Drawing.Size(80, 23);

            // lblThreads
            this.lblThreads.AutoSize = true;
            this.lblThreads.Location = new System.Drawing.Point(30, 70);
            this.lblThreads.Text = "Threads:";

            // numThreads
            this.numThreads.Location = new System.Drawing.Point(120, 68);
            this.numThreads.Minimum = 1;
            this.numThreads.Maximum = 50;
            this.numThreads.Value = 5;
            this.numThreads.Size = new System.Drawing.Size(80, 23);

            // rbSafe
            this.rbSafe.AutoSize = true;
            this.rbSafe.Location = new System.Drawing.Point(240, 30);
            this.rbSafe.Text = "Safe Mode";
            this.rbSafe.Checked = true;

            // rbUnsafe
            this.rbUnsafe.AutoSize = true;
            this.rbUnsafe.Location = new System.Drawing.Point(240, 60);
            this.rbUnsafe.Text = "Unsafe Mode";

            // btnStart
            this.btnStart.Location = new System.Drawing.Point(400, 25);
            this.btnStart.Size = new System.Drawing.Size(100, 30);
            this.btnStart.Text = "Start";
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);

            // btnStop
            this.btnStop.Location = new System.Drawing.Point(400, 65);
            this.btnStop.Size = new System.Drawing.Size(100, 30);
            this.btnStop.Text = "Stop";
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);

            // btnClear
            this.btnClear.Location = new System.Drawing.Point(520, 45);
            this.btnClear.Size = new System.Drawing.Size(100, 30);
            this.btnClear.Text = "Clear Log";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);

            // txtLog
            this.txtLog.Location = new System.Drawing.Point(30, 120);
            this.txtLog.Multiline = true;
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLog.Size = new System.Drawing.Size(720, 280);
            this.txtLog.ReadOnly = true;

            // MainForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lblAccounts);
            this.Controls.Add(this.numAccounts);
            this.Controls.Add(this.lblThreads);
            this.Controls.Add(this.numThreads);
            this.Controls.Add(this.rbSafe);
            this.Controls.Add(this.rbUnsafe);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.txtLog);
            this.Text = "Bank Transfer Simulator";

            ((System.ComponentModel.ISupportInitialize)(this.numAccounts)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numThreads)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
    }
}
