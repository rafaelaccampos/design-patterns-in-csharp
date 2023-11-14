namespace DesignPatterns.Behavioral.TemplateMethod_Beverage
{
    public abstract class TaxItem : Item
    {
        protected TaxItem(string category, string description, decimal price) 
            : base(category, description, price)
        {
        }

        public decimal CalculateTax()
        {
            return (Price * GetTaxeRate()) / 100;
        }

        public abstract decimal GetTaxeRate();
    }
}
