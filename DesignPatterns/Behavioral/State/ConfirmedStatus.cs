namespace DesignPatterns.Behavioral.State
{
    public class ConfirmedStatus : OrderStatus
    {
        private readonly Order _order;

        public ConfirmedStatus(Order order)
            : base(order)
        {
            Value = "Confirmed";
            _order = order;
        }

        public override string Value { get; set; } = null!;

        public override void Confirm()
        {
            throw new Exception("The order is already confirmed!");
        }
        
        public override void Cancel()
        {
            _order.Status = new CancelledStatus(_order);
        }
    }
}
