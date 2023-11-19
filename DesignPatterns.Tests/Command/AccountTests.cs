using DesignPatterns.Behavioral.Command_Transaction;
using FluentAssertions;

namespace DesignPatterns.Tests.Command
{
    public class AccountTests
    {
        [Test]
        public void ShouldBeAbleToCreateAnAccount()
        {
            var account = new Account();
            var balance = account.GetBalance();

            balance.Should().Be(0);
        }

        [Test]
        public void ShouldBeAbleToCreditFromAnAccountUsingCommand()
        {
            var account = new Account();
            var creditCommand = new CreditCommand(account, 100);
            
            creditCommand.Execute();

            var balance = account.GetBalance();
            balance.Should().Be(100);
        }

        [Test]
        public void ShouldBeAbleToDebitFromAnAccountUsingCommand()
        {
            var account = new Account();
            var creditCommand = new CreditCommand(account, 100);
            var debitCommand = new DebitCommand(account, 50);

            creditCommand.Execute();
            debitCommand.Execute();

            var balance = account.GetBalance();
            balance.Should().Be(50);
        }
    }
}
