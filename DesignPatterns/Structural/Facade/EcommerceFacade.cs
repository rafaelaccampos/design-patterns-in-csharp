namespace DesignPatterns.Structural.Facade
{
    public class EcommerceFacade
    {
        private readonly IPaymentService _paymentService;
        private readonly IShippingService _shippingService;
        private readonly IInventoryService _inventoryService;

        public EcommerceFacade(
            IPaymentService paymentService, 
            IShippingService shippingService, 
            IInventoryService inventoryService)
        {
            _paymentService = paymentService;
            _shippingService = shippingService;
            _inventoryService = inventoryService;
        }

        public bool PlaceOrder(
            string productId, 
            int quantity, 
            string creditCardNumber, 
            string address)
        {
            if(!_inventoryService.CheckStock(productId))
            {
                return false;
            }

            var paymentSucessful = _paymentService.ProcessPayment(creditCardNumber, quantity * 100);

            if(!paymentSucessful)
            {
                return false;
            }

            var shippingCost = _shippingService.CalculateShippingCost(address);
            var shippingLabel = _shippingService.GenerateShippingLabel(address);

            return true;
        }
    }
}
