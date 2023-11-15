namespace DesignPatterns.Behavioral.TemplateMethod_Beverage
{
    public class Water : Item
    {
        public Water(string description, decimal price) 
            : base("Water", description, price)
        {
        }
    }
}
