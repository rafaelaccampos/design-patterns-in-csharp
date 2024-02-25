namespace DesignPatterns.Behavioral.Memento
{
    public class ShoppingCartOriginator : IShoppingCartOriginator
    {
        public ShoppingCartOriginator(Guid customerId, IList<OrderItemInput> items)
        {
            CustomerId = customerId;
            Items = items.ToDictionary(item => item.ProductId, item => item.Quantity);
        }
            
        public Guid CustomerId { get; private set; }

        public Dictionary<Guid, int> Items { get; private set; }

        public void Restore(IShoppingCartMemento shoppingCartMemento)
        {
            var shoppingCart = shoppingCartMemento as ShoppingCartMemento;
            Items = shoppingCart!.Items;
        }

        public void UpdateCart(IList<OrderItemInput> items)
        {
            Items = items.ToDictionary(item => item.ProductId, item => item.Quantity);
        }

        public IShoppingCartMemento SaveSnapshot()
        {
            return new ShoppingCartMemento(CustomerId, Items);
        }
    }
}
