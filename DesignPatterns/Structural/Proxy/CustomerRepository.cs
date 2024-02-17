namespace DesignPatterns.Structural.Proxy
{
    public class CustomerRepository
    {
        public virtual IList<Customer> GetBlockedUsers()
        {
            return new List<Customer>
            {
                new Customer("Fulano", DateTime.Now.AddYears(-20)),
                new Customer("Fulano", DateTime.Now.AddYears(-30)),
                new Customer("Fulano", DateTime.Now.AddYears(-40))
            };
        }
    }
}
