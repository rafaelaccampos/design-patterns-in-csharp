
using System.Collections;

namespace DesignPatterns.Behavioral.Iterator
{
    public class CustomerToNotifyQueryModel : IEnumerable<KeyValuePair<string, string>>
    {
        private Dictionary<string, string> _customers;

        public CustomerToNotifyQueryModel(IList<Customer> customer, string generatedBy)
        {
            _customers = customer.ToDictionary(c => c.FullName, c => c.Email);
            GeneratedAt = DateTime.Now;
            GeneratedBy = generatedBy;
        }

        public string this[string customerFullName]
        {
            get {
                if (_customers.ContainsKey(customerFullName))
                {
                    return _customers[customerFullName];
                }
                return null!;
            }
        }
        public DateTime GeneratedAt { get; set; }

        public string GeneratedBy { get; set; }

        public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
        {
            return _customers.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
