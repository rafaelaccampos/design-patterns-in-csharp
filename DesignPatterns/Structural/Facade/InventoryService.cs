namespace DesignPatterns.Structural.Facade
{
    public class InventoryService : IInventoryService
    {
        public bool CheckStock(string productId)
        {
            return true;
        }
    }
}
