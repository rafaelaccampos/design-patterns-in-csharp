using DesignPatterns.Structural.Facade;
using FluentAssertions;
using FluentAssertions.Execution;
using NSubstitute;

namespace DesignPatterns.Tests.Facade
{
    [TestFixture]
    public class EcommerceFacadeTests
    {
        private IPaymentService _paymentService;
        private IShippingService _shippingService;
        private IInventoryService _inventoryService;
        private EcommerceFacade _ecommerceFacade;

        [SetUp] 
        public void SetUp()
        {
            _paymentService = Substitute.For<IPaymentService>();
            _shippingService = Substitute.For<IShippingService>();
            _inventoryService = Substitute.For<IInventoryService>();

            _ecommerceFacade = new EcommerceFacade(_paymentService, _shippingService, _inventoryService);
        }

        [Test]
        public void ShouldReturnFalseWhenProductIsOutOfStock()
        {
            _inventoryService.CheckStock(Arg.Any<string>()).Returns(false);

            var placeOrder = _ecommerceFacade.PlaceOrder("Produto123", 2, "1233331313", "Rua Horacio");

            placeOrder.Should().BeFalse();
        }

        [Test]
        public void ShouldReturnTrueWhenAllServicesSucceed()
        {
            var inventoryService = Substitute.For<IInventoryService>();
            var paymentService = Substitute.For<IPaymentService>();
            var shippingService = Substitute.For<IShippingService>();

            inventoryService.CheckStock(Arg.Any<string>()).Returns(true);
            paymentService.ProcessPayment(Arg.Any<string>(), Arg.Any<decimal>()).Returns(true);
            shippingService.CalculateShippingCost(Arg.Any<string>()).Returns(10.0m);
            shippingService.GenerateShippingLabel(Arg.Any<string>()).Returns("EtiquetaDeEnvio2024");

            var facade = new EcommerceFacade(paymentService, shippingService, inventoryService);

            var placeOrder = facade.PlaceOrder("productId", 1, "1234567890", "address");

            using (new AssertionScope())
            {
                placeOrder.Should().BeTrue();
                inventoryService.Received(1).CheckStock("productId");
                paymentService.Received(1).ProcessPayment("1234567890", 100);
                shippingService.Received(1).GenerateShippingLabel("address");
            }
        }
    }
}
