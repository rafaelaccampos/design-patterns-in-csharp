namespace DesignPatterns.Structural.Adapter.Adapter_Transaction
{
    public class PayPalTransactionAdapter : Transaction
    {
        public PayPalTransactionAdapter(PaypalTransaction payPalTransaction)
        {
            TrackNumber = payPalTransaction.Id.ToString();
            Amount = payPalTransaction.Amount;
            Status = payPalTransaction.Status;
        }

        public string TrackNumber { get; private set; }

        public decimal Amount { get; private set; }

        public string Status { get; private set; }
    }
}
