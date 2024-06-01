using DesignPatterns.Behavioral.Memento;

namespace DesignPatterns.Creational.Factory_Method
{
    public class PaymentSlipService
    {
        public object Process(OrderItemInput orderInput)
        {
            return "Dados do boleto";
        }
    }
}
