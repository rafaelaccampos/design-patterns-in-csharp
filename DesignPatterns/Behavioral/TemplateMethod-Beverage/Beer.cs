namespace DesignPatterns.Behavioral.TemplateMethod_Beverage
{
    public class Beer : TaxItem
    {
        public Beer(string description, decimal price) 
            : base("Beer", description, price)
        {
        }

        public override decimal GetTaxeRate()
        {
            return 10;
        }
    }
}
