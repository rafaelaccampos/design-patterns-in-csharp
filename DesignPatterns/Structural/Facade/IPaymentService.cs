namespace DesignPatterns.Structural.Facade
{
    public interface IPaymentService
    {
        bool ProcessPayment(string creditCardNumber, decimal amount);
    }
}
