using DesignPatterns.Behavioral.Memento;

namespace DesignPatterns.Creational.Factory_Method
{
    public class Order
    {
        private readonly IPaymentServiceFactory _paymentServiceFactory;

        public Order(IPaymentServiceFactory paymentServiceFactory)
        {
            _paymentServiceFactory = paymentServiceFactory;
        }
        public void Create(OrderItem orderItem)
        {
            var paymentService = _paymentServiceFactory.GetService(orderItem.PaymentMethod);

            paymentService.Process(orderItem);
        }
    }
}
