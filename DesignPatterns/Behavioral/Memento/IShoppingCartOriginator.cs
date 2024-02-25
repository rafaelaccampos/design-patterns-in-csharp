namespace DesignPatterns.Behavioral.Memento
{
    public interface IShoppingCartOriginator
    {
        Guid CustomerId { get; }

        Dictionary<Guid, int> Items { get; }

        void Restore(IShoppingCartMemento shoppingCartMemento);

        void UpdateCart(IList<OrderItemInput> items);

        IShoppingCartMemento SaveSnapshot();
    }
}
