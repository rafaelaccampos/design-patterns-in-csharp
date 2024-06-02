using DesignPatterns.Behavioral.Memento;

namespace DesignPatterns.Creational.Factory_Method
{
    public class CreditCardService : IPaymentService
    {
        public object Process(OrderItem orderItem)
        {
            return "Transação aprovada!";
        }
    }
}
