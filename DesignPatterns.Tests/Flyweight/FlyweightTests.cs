using DesignPatterns.Structural.Flyweight;
using FluentAssertions;

namespace DesignPatterns.Tests.Flyweight
{
    public class FlyweightTests
    {
        [Test]
        public void ShouldBeAbleToReturnTheSameInstanceForTheSamePaymentMethod()
        {
            var paymentMethodFactory = new PaymentsMethodFactory();

            var firstCreditCard = paymentMethodFactory.GetPaymentMethod(PaymentMethod.CreditCard);
            var secondCreditCard = paymentMethodFactory.GetPaymentMethod(PaymentMethod.CreditCard);

            firstCreditCard.Should().Be(secondCreditCard);
        }

        [Test]
        public void ShouldBeAbleToReturnDifferentInstancesForDifferentPaymentMethods()
        {
            var paymentFactory = new PaymentsMethodFactory();

            var paymentSlip = paymentFactory.GetPaymentMethod(PaymentMethod.CreditCard);
            var payPal = paymentFactory.GetPaymentMethod(PaymentMethod.PayPal);

            paymentSlip.Should().NotBe(payPal);
        }
    }
}
