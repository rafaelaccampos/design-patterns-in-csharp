namespace DesignPatterns.Behavioral.TemplateMethod_Beverage
{
    public class Whisky : TaxItem
    {
        public Whisky(string description, decimal price) 
            : base("Whisky", description, price)
        {
        }

        public override decimal GetTaxeRate()
        {
            return 20;
        }
    }
}
