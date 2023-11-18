using System.Text;

namespace DesignPatterns.Creational.Builder
{
    public class ItemBuilder
    {
        public ItemBuilder(
            int idItem, 
            string category, 
            string description, 
            decimal price)
        {
            IdItem = idItem;
            Category = category;
            Description = description;
            Price = price;
        }

        public int IdItem { get; private set; }

        public string Category { get; private set; }

        public string Description { get; private set; }

        public decimal Price { get; private set; }

        public decimal Width { get; private set; }

        public decimal Height { get; private set; }

        public decimal Length { get; private set; }

        public decimal Weight { get; private set; }


        public ItemBuilder WithWidth(decimal width)
        {
            Width = width;
            return this;
        }

        public ItemBuilder WithHeight(decimal height)
        {
            Height = height;
            return this;
        }

        public ItemBuilder WithLenght(decimal lenght)
        {
            Length = lenght;
            return this;
        }

        public ItemBuilder WithWeight(decimal weight)
        {
            Weight = weight;
            return this;
        }

        public Item Generate()
        {
            var item = new Item(this);
            return item;
        }
    }
}
