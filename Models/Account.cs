using System;

namespace BankTransferSimulator.Models
{
    /// <summary>
    /// Represents a bank account with a balance and a dedicated lock object
    /// to manage concurrent access. The Withdraw/Deposit methods are intentionally
    /// NOT thread-safe internally, relying on external locking (in Bank.cs) 
    /// to demonstrate the need for proper lock management.
    /// </summary>
    public class Account
    {
        public int Id { get; }
        public decimal Balance { get; private set; }

        // Dedicated lock object for this specific account
        private readonly object _lock = new object();

        public Account(int id, decimal balance)
        {
            Id = id;
            Balance = balance;
        }

        /// <summary>Returns the private lock object for this account.</summary>
        public object GetLock() => _lock;

        /// <summary>
        /// Withdraws an amount. This method is NOT thread-safe and MUST be 
        /// called within a lock provided by GetLock().
        /// </summary>
        public void Withdraw(decimal amount)
        {
            // The race condition for data corruption occurs here if external locking is absent:
            // 1. Thread A checks Balance >= amount.
            // 2. Thread B checks Balance >= amount.
            // 3. Both proceed to subtract, potentially overdrawing or creating money.
            if (Balance >= amount)
                Balance -= amount;
        }

        /// <summary>
        /// Deposits an amount. This method is NOT thread-safe and MUST be 
        /// called within a lock provided by GetLock().
        /// </summary>
        public void Deposit(decimal amount)
        {
            Balance += amount;
        }
    }
}