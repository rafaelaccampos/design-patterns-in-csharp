using DesignPatterns.Behavioral.Memento;
using FluentAssertions;

namespace DesignPatterns.Tests.Memento
{
    public class ShoppingCartCareTakerTests
    {
        [Test]
        public void ShouldBeAbleToMakeBackUpAndUndo()
        {
            var customerId = Guid.NewGuid();
            var orderItemsInput = new List<OrderItemInput>
            {
                new OrderItemInput { ProductId = Guid.NewGuid(), Quantity = 1 },
                new OrderItemInput { ProductId = Guid.NewGuid(), Quantity = 3 },
            };
            var originator = new ShoppingCartOriginator(customerId, orderItemsInput);
            var careTaker = new ShoppingCartCaretaker(originator);

            careTaker.Backup();
            originator.UpdateCart(new List<OrderItemInput>
            {
                new OrderItemInput { ProductId = Guid.NewGuid(), Quantity = 3 },
            });
            careTaker.Backup();
            careTaker.Undo();

            originator.Items.Count.Should().Be(2);
        }

        [Test]
        public void ShouldBeAbleToMakePrintHistory()
        {
            var customerId = Guid.NewGuid();
            var orderItemsInput = new List<OrderItemInput>
            {
                new OrderItemInput { ProductId = Guid.NewGuid(), Quantity = 1 },
            };
            var originator = new ShoppingCartOriginator(customerId, orderItemsInput);
            var careTaker = new ShoppingCartCaretaker(originator);

            careTaker.Backup();

            using (StringWriter stringWriter = new StringWriter())
            {
                Console.SetOut(stringWriter);
                careTaker.PrintHistory();
                var expected = @$"Customer: {customerId}, Items: > Item: {originator.Items.First().Key}, Quantity: {originator.Items.First().Value} ";
                stringWriter.ToString().Trim().Should().Be(expected.Trim());
            }
        }
    }
}
