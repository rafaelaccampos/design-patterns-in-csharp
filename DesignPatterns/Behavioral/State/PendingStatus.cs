﻿namespace DesignPatterns.Behavioral.State
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
            _order.Status = new ConfirmedStatus(_order);
        }

        public override void Cancel()
        {
            _order.Status = new CancelledStatus(_order);
        }
    }
}
