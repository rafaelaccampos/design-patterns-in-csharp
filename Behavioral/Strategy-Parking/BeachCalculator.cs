namespace DesignPatterns.Behavioral.Strategy
{
    public class BeachCalculator : ITicketCalculator
    {
        private const int HOURLY_RATE = 5;
        public int Calculate(Period period)
        {
            return HOURLY_RATE * period.GetDiffInHours();
        }
    }
}
