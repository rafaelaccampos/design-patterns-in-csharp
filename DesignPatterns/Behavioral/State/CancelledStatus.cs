namespace DesignPatterns.Behavioral.State
{
    public class CancelledStatus : OrderStatus
    {
        private readonly Order _order;

        public CancelledStatus(Order order)
            : base(order)
        {
            _order = order;
            Value = "Cancelled";
        }

        public override string Value { get; set; }

        public override void Confirm()
        {
            throw new Exception("The order is already cancelled!");
        }

        public override void Cancel()
        {
            throw new Exception("The order is already cancelled!");
        }
    }
}
