using DesignPatterns.Creational.Factory_Method;
using FluentAssertions;

namespace DesignPatterns.Tests
{
    public class PaymentServiceFactoryTests
    {
        [Test]
        public void Create_ShouldProcessOrderWithPaymentSlipService()
        {
            var factory = new PaymentServiceFactory();
            var order = new Order(factory);
            var orderItem = new OrderItem
            {
                ProductId = Guid.NewGuid(),
                Quantity = 1,
                PaymentMethod = PaymentMethod.PaymentSlip
            };

            order.Create(orderItem);

            var service = factory.GetService(orderItem.PaymentMethod);
            var paymentSlip = service.Process(orderItem);

            paymentSlip.Should().Be("Dados do boleto");
        }
    }
}
