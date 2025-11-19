using System;
using System.Windows.Forms;
using BankTransferSimulator.Forms;

namespace BankTransferSimulator
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}