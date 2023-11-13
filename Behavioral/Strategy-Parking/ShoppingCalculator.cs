namespace DesignPatterns.Behavioral.Strategy_Parking
{
    public class ShoppingCalculator : ITicketCalculator
    {
        private const long BASE_RATE = 10;
        private const int BASE_PERIOD = 3;
        private const int HOURLY_RATE = 3;

        public long Calculate(Period period)
        {
            var price = BASE_RATE;
            var remainingHours = period.GetDiffInHours() - BASE_PERIOD;

            if(remainingHours > 0)
            {
                price += remainingHours * HOURLY_RATE;
            }

            return price;
        }
    }
}
