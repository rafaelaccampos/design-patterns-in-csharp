namespace DesignPatterns.Structural.Facade
{
    public class ShippingService : IShippingService
    {
        public decimal CalculateShippingCost(string address)
        {
            return 10.0m;
        }

        public string GenerateShippingLabel(string address)
        {
            return "EtiquetaDeEnvio2024";
        }
    }
}
