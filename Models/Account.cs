
namespace BankTransferSimulator.Models
{
    public class Account
    {
        public int Id { get; }      
        public decimal Balance { get; private set; }      
        private readonly object _lock = new object();
        public Account(int id, decimal balance)
        {
            Id = id;
            Balance = balance;
        }

        public object GetLock() => _lock;

        public void Withdraw(decimal amount)
        {
            if (Balance >= amount)
                Balance -= amount;
        }

        public void Deposit(decimal amount)
        {
            Balance += amount;
        }
    }
}