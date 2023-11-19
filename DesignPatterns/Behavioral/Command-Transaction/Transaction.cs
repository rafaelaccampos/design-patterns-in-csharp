namespace DesignPatterns.Behavioral.Command_Transaction
{
    public class Transaction
    {
        public Transaction(string type, decimal amount) 
        {
            Type = type;
            Amount = amount;
        }

        public string Type { get; private set; }

        public decimal Amount { get; private set; }
    }
}
