namespace DesignPatterns.Structural.Adapter.Adapter_Transaction
{
    public class StripeTransactionAdapter : Transaction
    {
        public StripeTransactionAdapter(StripeTransaction stripeTransaction)
        {
            TrackNumber = stripeTransaction.Code;
            Amount = stripeTransaction.GrossAmount;
            Status = stripeTransaction.Situation.ToString();
        }

        public string TrackNumber { get; private set; }

        public int Amount { get; private set; }

        public string Status { get; private set; }
    }
}
