namespace DesignPatterns.Structural.Adapter.Adapter_Transaction
{
    public class StripeTransactionAdapter : ITransaction
    {
        public StripeTransactionAdapter(StripeTransaction stripeTransaction)
        {
            TrackNumber = stripeTransaction.Code;
            Amount = stripeTransaction.GrossAmount;
            Status = ConvertStatus(stripeTransaction.Situation);
        }

        public string TrackNumber { get; private set; }

        public decimal Amount { get; private set; }

        public string Status { get; private set; }

        public string ConvertStatus(int status)
        {
            switch (status)
            {
                case 1:
                    return "waiting_payment";
                case 2:
                    return "paid";
                case 3:
                    return "cancelled";
                default:
                    return string.Empty;
            }
        }
    }
}
