using DesignPatterns.Structural.Decorator;

namespace DesignPatterns.Tests.Decorator
{
    public class DecoratorTests
    {
        [Test]
        public void ShouldBeAbleToLogOperationsInAUseCase()
        {
            var input = new Input
            {
                Cpf = "111111111",
                Items = new List<Item>
                {
                    new Item { Id = 1, Count = 1 },
                    new Item { Id = 2, Count = 1 },
                    new Item { Id = 3, Count = 3 },
                }
            };

            var placeOrder = new LoggerUseCaseDecorator(new PerformanceUseCaseDecorator(new PlaceOrder()));
            placeOrder.Execute(input);

            var simulateFreight = new LoggerUseCaseDecorator(new PerformanceUseCaseDecorator(new SimulateFreight()));
            simulateFreight.Execute(input);
        }
    }
}
