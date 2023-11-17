namespace DesignPatterns.Behavioral.State
{
    public abstract class OrderStatus
    {
        private readonly Order _order;

        public OrderStatus()
        {
            _order = new Order();
        }

        public abstract string Value { get; set; }

        public abstract void Confirm();

        public abstract void Cancel();
    }
}
