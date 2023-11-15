using DesignPatterns.Structural.Adapter.Adapter_Transaction;
using FluentAssertions;
using FluentAssertions.Execution;

namespace DesignPatterns.Tests.Adapter
{
    public class TransactionTests
    {
        [Test]
        public void ShouldBeAbleToCreateAStripeTransaction()
        {
            var stripeTransaction = new StripeTransaction("AHN765NHD89", 1000, 2);
            stripeTransaction.Code.Should().Be("AHN765NHD89");
        }

        [Test]
        public void ShouldBeAbleToCreateAPayPalTransaction()
        {
            var payPalTransaction = new PaypalTransaction(7897897, 1000, "S");
            payPalTransaction.Id.Should().Be(7897897);
        }

        [Test]
        public void ShouldBeAbleToCreateTransactionFromStripe()
        {
            var stripeTransaction = new StripeTransaction("AHN765NHD89", 1000, 2);
            var transaction = new StripeTransactionAdapter(stripeTransaction);

            using (new AssertionScope())
            {
                transaction.TrackNumber.Should().Be("AHN765NHD89");
                transaction.Amount.Should().Be(1000);
                transaction.Status.Should().Be("paid");
            }
        }

        [Test]
        public void ShouldBeAbleToCreateTransactionFromPaypal()
        {
            var payPalTransaction = new PaypalTransaction(7897897, 1000, "S");
            var transaction = new PayPalTransactionAdapter(payPalTransaction);

            using (new AssertionScope())
            {
                transaction.TrackNumber.Should().Be("7897897");
                transaction.Amount.Should().Be(1000);
                transaction.Status.Should().Be("paid");
            }
        }
    }
}