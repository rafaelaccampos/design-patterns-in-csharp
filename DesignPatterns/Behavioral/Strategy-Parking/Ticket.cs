﻿namespace DesignPatterns.Behavioral.Strategy_Parking
{
    public class Ticket
    {
        public string Plate { get; set; } = null!;
        public DateTime CheckInDate { get; set; }
        public decimal? Price { get; set; }
    }
}
