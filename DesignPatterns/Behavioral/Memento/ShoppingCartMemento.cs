namespace DesignPatterns.Behavioral.Memento
{
    public class ShoppingCartMemento : IShoppingCartMemento
    {
        public ShoppingCartMemento(Guid customerId, Dictionary<Guid, int> items)
        {
            CustomerId = customerId;
            Items = items;
            SavedAt = DateTime.UtcNow;
        }

        public Guid CustomerId { get; private set; }

        public Dictionary<Guid, int> Items { get; private set; }

        public DateTime SavedAt { get; private set; }
    }
}
