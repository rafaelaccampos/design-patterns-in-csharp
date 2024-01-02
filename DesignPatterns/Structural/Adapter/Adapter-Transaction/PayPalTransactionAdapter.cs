namespace DesignPatterns.Structural.Adapter.Adapter_Transaction
{
    public class PayPalTransactionAdapter : ITransaction
    {
        public PayPalTransactionAdapter(PaypalTransaction payPalTransaction)
        {
            TrackNumber = payPalTransaction.Id.ToString();
            Amount = payPalTransaction.Amount;
            Status = ConvertStatus(payPalTransaction.Status);
        }

        public string TrackNumber { get; private set; }

        public decimal Amount { get; private set; }

        public string Status { get; private set; }

//TODO: I can use dictionary here instead of switch
        public string ConvertStatus(string status)
        {
            switch (status)
            {
                case "P":
                    return "waiting_payment";
                case "S":
                    return "paid";
                case "F":
                    return "refunded";
                default:
                    return string.Empty;
            }
        }
    }
}
