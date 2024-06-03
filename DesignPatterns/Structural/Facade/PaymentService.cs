namespace DesignPatterns.Structural.Facade
{
    public class PaymentService : IPaymentService
    {
        public bool ProcessPayment(string creditCardNumber, decimal amount)
        {
            return true;
        }
    }
}
