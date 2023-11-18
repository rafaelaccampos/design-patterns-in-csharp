namespace DesignPatterns.Behavioral.State
{
    public abstract class OrderStatus
    {
        private readonly Order _order;

        public OrderStatus(Order order)
        {
            _order = order;
        }

        public abstract string Value { get; set; }

        public abstract void Confirm();

        public abstract void Cancel();
    }
}
