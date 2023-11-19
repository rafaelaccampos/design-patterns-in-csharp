namespace DesignPatterns.Behavioral.Command_Transaction
{
    public class Account
    {
        private readonly IList<Transaction> _transactions;

        public Account()
        {
            _transactions = new List<Transaction>();
        }

        public void Credit(decimal amount)
        {
            var transaction = new Transaction("credit", amount);
            _transactions.Add(transaction);
        }

        public void Debit(decimal amount)
        {
            var transaction = new Transaction("debit", amount);
            _transactions.Add(transaction);
        }

        public decimal GetBalance()
        {
            decimal balance = 0;
            foreach (var transaction in _transactions)
            {
                if(transaction.Type == "credit")
                {
                    balance += transaction.Amount;
                }

                if(transaction.Type == "debit")
                {
                    balance -= transaction.Amount;
                }
            }

            return balance;
        }
    }
}
