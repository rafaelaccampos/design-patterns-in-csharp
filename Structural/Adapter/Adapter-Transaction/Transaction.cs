namespace DesignPatterns.Structural.Adapter.Adapter_Transaction
{
    public interface Transaction
    {
        public string TrackNumber { get; }
        public decimal Amount { get; }
        public string Status { get; }
    }
}
