namespace DesignPatterns.Behavioral.Strategy_Parking
{
    public class AiportCalculator : ITicketCalculator
    {
        private const int DAILY_RATE = 50;

        public long Calculate(Period period)
        {
            return DAILY_RATE * period.GetDiffInDays();
        }
    }
}
