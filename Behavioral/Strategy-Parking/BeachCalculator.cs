﻿namespace DesignPatterns.Behavioral.Strategy_Parking
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
