namespace DesignPatterns.Structural.Facade
{
    public class AntiFraudInput
    {
        public AntiFraudInput(string document, decimal totalAmount)
        {
            Document = document;
            TotalAmount = totalAmount;
        }

        public string Document { get; set; }

        public decimal TotalAmount { get; set; }
    }
}
