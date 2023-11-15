namespace DesignPatterns.Behavioral.TemplateMethod_Beverage
{
    public abstract class Item
    {
        public Item(string category, string description, decimal price)
        {
            Category = category;
            Description = description;
            Price = price;
        }

        public string Category { get; private set; }

        public string Description { get; private set; }

        public decimal Price { get; private set; }
    }
}
