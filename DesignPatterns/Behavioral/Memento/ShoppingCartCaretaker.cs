namespace DesignPatterns.Behavioral.Memento
{
    public class ShoppingCartCaretaker
    {
        public readonly ShoppingCartOriginator Originator;
        public readonly IList<IShoppingCartMemento> _mementos;

        public ShoppingCartCaretaker(ShoppingCartOriginator originator)
        {
            Originator = originator;
            _mementos = new List<IShoppingCartMemento>();
        }

        public void Backup()
        {
            _mementos.Add(Originator.SaveSnapshot());
        }

        public void Undo()
        {
            if(_mementos.Count == 0)
            {
                return;
            }

            var lastMemento = _mementos.Last();

            Originator.Restore(lastMemento);
        }

        public void PrintHistory()
        {
            foreach(var memento in _mementos)
            {
                var items = string.Join(' ', memento.Items.Select(i => $"> Item: {i.Key}, Quantity: {i.Value}"));
                Console.WriteLine($"Customer: {memento.CustomerId}, Items: {items}\n");
            }
        }
    }
}
