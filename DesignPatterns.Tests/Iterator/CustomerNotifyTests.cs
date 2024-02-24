using DesignPatterns.Behavioral.Iterator;

namespace DesignPatterns.Tests.Iterator
{
    public class CustomerNotifyTests
    {
        [Test]
        public void ShouldBeAbleToIterateThroughCustomers()
        {
            var customers = new List<Customer>
            {
                new Customer { FullName = "Fulano", Email = "fulano@example.com" },
                new Customer { FullName = "Fulana", Email = "fulana@example.com" },
            };

            var customerToNotify = new CustomerToNotify(customers, "me");

            var notifiedCustomers = new Dictionary<string, string>();

            foreach (var customer in customers)
            {
                notifiedCustomers[customer.FullName] = customer.Email;
            }

           CollectionAssert.AreEquivalent(notifiedCustomers, customerToNotify);
        }
    }
}
