using DesignPatterns.Behavioral.Strategy;
using System.Runtime.InteropServices;

namespace DesignPatterns.Behavioral.Strategy_Parking
{
    public class TicketCalculatorFactory
    {
        public static ITicketCalculator Create(string location)
        {
            switch(location)
            {
                case "beach":
                    {
                        return new BeachCalculator();
                    }
                case "shopping":
                    {
                        return new ShoppingCalculator();
                    }
                case "airport":
                    {
                        return new AiportCalculator();
                    }
            }
            throw new Exception("Ticket calculator not found!");
        }
    }
}
