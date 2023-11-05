namespace DesignPatterns.Structural.Adapter.Adapter_Transaction
{
    public class PaypalTransaction
    {
        public PaypalTransaction(int id, int amount, string status)
        {
            Id = id;
            Amount = amount;
            Status = status;
        }

        public int Id { get; private set; }
        public decimal Amount { get; private set; }
        public string Status { get; private set; }
    }
}
