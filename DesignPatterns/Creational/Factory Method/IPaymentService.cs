namespace DesignPatterns.Creational.Factory_Method
{
    public interface IPaymentService
    {
        object Process(OrderItem orderItem);
    }
}
