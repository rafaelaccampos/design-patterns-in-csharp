namespace DesignPatterns.Behavioral.TemplateMethod_Beverage
{
    public class Invoice
    {
        private readonly IList<Item> _items;

        public Invoice()
        {
            _items = new List<Item>();
        }

        public void Add(Item item)
        {
            _items.Add(item);
        }

        public decimal GetTaxes()
        {
            return _items.Where(item => item is TaxItem)
                .Cast<TaxItem>()
                .Sum(taxItem => taxItem.CalculateTax());
        }
    }
}
