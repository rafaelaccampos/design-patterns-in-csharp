using DesignPatterns.Behavioral.State;
using FluentAssertions;

namespace DesignPatterns.Tests.State
{
    public class OrderStatusTests
    {
        [Test]
        public void ShouldBeAbleToCreateAnPedingStatus()
        {
            var order = new Order();
            order.Status.Value.Should().Be("Pending");
        }

        [Test]
        public void ShouldBeAbleToChangeOrderStatusToConfirmed()
        {
            var order = new Order();
            order.Confirm();

            order.Status.Value.Should().Be("Confirmed");
        }

        [Test]
        public void ShouldBeAbleToChangeOrderStatusToCancelled()
        {
            var order = new Order();
            order.Cancel();

            order.Status.Value.Should().Be("Cancelled");
        }

        [Test]
        public void ShouldNotBeAbleToChangeOrderStatusToConfirmedIfTheOrderHasAlreadyBeenCancelled()
        {
            var order = new Order();
            order.Cancel();

            order.Invoking(o => o.Confirm())
                .Should().Throw<Exception>()
                .WithMessage("The order is already cancelled!");
        }
    }
}
