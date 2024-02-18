namespace DesignPatterns.Structural.Bridge
{
    public class Food : Item
    {
        public Food(IUnit unit) 
            : base(unit)
        {
        }

        public string NutritionLabel { get; set; }

        public override decimal GetTotalPrice(decimal units)
        {
            if(!_unit.Validate(units))
            {
                throw new ArgumentException();
            }

            return units * PricePerUnit;
        }
    }
}
