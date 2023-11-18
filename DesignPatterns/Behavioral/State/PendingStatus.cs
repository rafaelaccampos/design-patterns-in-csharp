namespace DesignPatterns.Behavioral.State
{
    public class PendingStatus : OrderStatus
    {
        private readonly Order _order;
        public PendingStatus(Order order)
            : base(order)
        {
            _order = order;
            Value = "Pending";
        }

        public override string Value { get; set; } = null!;

        public override void Confirm()
        {
            throw new NotImplementedException();
        }

        public override void Cancel()
        {
            throw new NotImplementedException();
        }
    }
}
