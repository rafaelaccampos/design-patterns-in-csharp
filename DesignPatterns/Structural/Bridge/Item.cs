namespace DesignPatterns.Structural.Bridge
{
    public abstract class Item
    {
        protected readonly IUnit _unit;

        public Item(IUnit unit)
        {
            _unit = unit;
        }

        public string Title { get; set; } = null!;

        public decimal PricePerUnit { get; set; }

        public abstract decimal GetTotalPrice(decimal unit);
    }
}
