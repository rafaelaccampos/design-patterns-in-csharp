namespace DesignPatterns.Structural.Bridge
{
    public class Kg : IUnit
    {
        public decimal Minimum { get; set; }

        public decimal Maximum { get; set; }

        public bool Validate(decimal units)
        {
            return units < 10;
        }
    }
}
