namespace DesignPatterns.Behavioral.Strategy_Parking
{
    public class TicketCalculatorFactory
    {
        private static IDictionary<string, ITicketCalculator> calculators = new Dictionary<string, ITicketCalculator>()
        {
            {
                "beach", new BeachCalculator()
            },
            {
                "shopping", new ShoppingCalculator()
            },
            {
                "airport", new AiportCalculator()
            }
    };

        public static ITicketCalculator Create(string location)
        {
            if(!calculators.TryGetValue(location, out var findCalculator))
            {
                throw new Exception("Ticket calculator not found!");
            }

            return findCalculator;
        }
    }
}
