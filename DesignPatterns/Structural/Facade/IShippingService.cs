namespace DesignPatterns.Structural.Facade
{
    public interface IShippingService
    {
        decimal CalculateShippingCost(string address);

        string GenerateShippingLabel(string address);
    }
}
