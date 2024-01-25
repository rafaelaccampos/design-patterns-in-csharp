using DesignPatterns.Behavioral.Chain_of_Responsability_Adm;
using FluentAssertions;

namespace DesignPatterns.Tests.Chain_of_Responsability
{
    public class AtmTests
    {
        [Test]
        public void ShouldBeAbleToRetrieveMoneyWithAllNotes()
        {
            var handler1 = new BillHandler(null, 1);
            var handler2 = new BillHandler(handler1, 2);
            var handler5 = new BillHandler(handler2, 5);
            var handler10 = new BillHandler(handler5, 10);
            var handler20 = new BillHandler(handler10, 20);
            var handler50 = new BillHandler(handler20, 50);
            var handler100 = new BillHandler(handler50, 100);
            var atm = new Atm(handler100);

            var bills = atm.Withdraw(978);

            var expectedBills = new List<Bill>
            {
                { new Bill { Count = 9, Type = 100 } },
                { new Bill { Count = 1, Type = 50 } },
                { new Bill { Count = 1, Type = 20 } },
                { new Bill { Count = 0, Type = 10 } },
                { new Bill { Count = 1, Type = 5 } },
                { new Bill { Count = 1, Type = 2 } },
                { new Bill { Count = 1, Type = 1 } },
            };

            bills.Should().BeEquivalentTo(expectedBills);
        }

        [Test]
        public void ShouldBeAbleToRetrieveMoneyWithOnlyOneNotes()
        {
            var handler1 = new BillHandler(null, 1);
            var atm = new Atm(handler1);

            var bills = atm.Withdraw(978);

            var expectedBills = new List<Bill>
            {
                { new Bill { Count = 978, Type = 1 } },
            };

            bills.Should().BeEquivalentTo(expectedBills);
        }

        [Test]
        public void ShouldBeAbleToRetrieveMoneyWithOnlyFiveAndTenNotes()
        {
            var handler5 = new BillHandler(null, 5);
            var handler10 = new BillHandler(handler5, 10);

            var atm = new Atm(handler10);
            var bills = atm.Withdraw(500);

            var expectedBills = new List<Bill>
            {
                { new Bill { Count = 50, Type = 10 } },
                { new Bill { Count = 0, Type = 5 } },
            };

            bills.Should().BeEquivalentTo(expectedBills);
        }
    }
}
