namespace DesignPatterns.Behavioral.State
{
    public class Order
    {
        public Order()
        {
            Status = new PendingStatus(this);
        }

        public OrderStatus Status { get; set; }

        public void Confirm()
        {
            Status.Confirm();
        }

        public void Cancel()
        {
            Status.Cancel();
        }
    }
}
