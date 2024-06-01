namespace DesignPatterns.Creational.Factory_Method
{
    public class OrderItem
    {
        public Guid ProductId { get; set; }

        public int Quantity { get; set; }
        
        public PaymentMethod PaymentMethod { get; set; }
    }
}
