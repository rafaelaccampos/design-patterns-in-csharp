namespace DesignPatterns.Structural.Bridge
{
    public class Quantity : IUnit
    {
        public Quantity()
        {
            Minimum = 1;
            Maximum = 200;
        }

        public decimal Minimum { get; set; }

        public decimal Maximum { get; set; }

        public bool Validate(decimal units)
        {
            if (units % 1 != 0)
            {
                return false;
            }

            if(Minimum > units || Maximum < units)
            {
                return false;
            }

            return true;
        }
    }
}
