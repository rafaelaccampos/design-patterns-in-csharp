namespace DesignPatterns.Structural.Adapter.Adapter_Transaction
{
    public interface ITransaction
    {
        public string TrackNumber { get; }
        public decimal Amount { get; }
        public string Status { get; }
    }
}
