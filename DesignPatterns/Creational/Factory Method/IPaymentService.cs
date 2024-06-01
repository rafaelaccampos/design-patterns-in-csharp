using DesignPatterns.Behavioral.Memento;

namespace DesignPatterns.Creational.Factory_Method
{
    public interface IPaymentService
    {
        object Process(OrderItemInput orderItem);
    }
}
