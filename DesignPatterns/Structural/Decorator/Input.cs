namespace DesignPatterns.Structural.Decorator
{
    public class Input
    {
        public string Cpf { get; set; }
        public IList<Item> Items { get; set; }
    }

    public class Item
    {
        public int Id { get; set; }
        public int Count { get; set; }
    }
}
