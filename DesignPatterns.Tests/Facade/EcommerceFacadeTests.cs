using DesignPatterns.Structural.Facade;
using FluentAssertions;
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
    }
}
