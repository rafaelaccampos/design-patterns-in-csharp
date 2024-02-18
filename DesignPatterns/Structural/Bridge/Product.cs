namespace DesignPatterns.Structural.Bridge
{
    public class Product : Item
    {
        public Product(IUnit unit) 
            : base(unit)
        {
        }

        public string Category { get; set; } = null!;

        public override decimal GetTotalPrice(decimal units)
        {
            if (!_unit.Validate(units))
            {
                throw new ArgumentException();
            }

            return units * PricePerUnit;
        }
    }
}
