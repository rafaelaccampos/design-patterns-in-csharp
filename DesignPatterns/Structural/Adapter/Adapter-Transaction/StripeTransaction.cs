namespace DesignPatterns.Structural.Adapter.Adapter_Transaction
{
    public class StripeTransaction
    {
        public StripeTransaction(string code, decimal grossAmount, int situation)
        {
            Code = code;
            GrossAmount = grossAmount;
            Situation = situation;
        }

        public string Code { get; private set; }
        public decimal GrossAmount { get; private set; }
        public int Situation { get; private set; }
    }
}
