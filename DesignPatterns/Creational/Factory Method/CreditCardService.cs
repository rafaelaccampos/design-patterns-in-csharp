using DesignPatterns.Behavioral.Memento;

namespace DesignPatterns.Creational.Factory_Method
{
    public class CreditCardService : IPaymentService
    {
        public object Process(OrderItemInput orderItem)
        {
            return "Transação aprovada!";
        }
    }
}
