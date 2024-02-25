namespace DesignPatterns.Behavioral.Memento
{
    public interface IShoppingCartMemento
    {
        Guid CustomerId { get; }

        Dictionary<Guid, int> Items { get; }

        DateTime SavedAt { get; }
    }
}
