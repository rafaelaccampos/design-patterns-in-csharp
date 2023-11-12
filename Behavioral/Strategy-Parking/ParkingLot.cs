namespace DesignPatterns.Behavioral.Strategy_Parking
{
    public class ParkingLot
    {
        private readonly IList<Ticket> tickets;
        private readonly ITicketCalculator ticketCalculator;

        public ParkingLot(string location)
        {
            tickets = new List<Ticket>();
            ticketCalculator = TicketCalculatorFactory.Create(location);
        }

        public void CheckIn(Ticket ticket)
        {
            tickets.Add(ticket);
        }

        public void CheckOut(string plate, DateTime checkoutDate)
        {
            var ticket = GetTicket(plate);
            var period = new Period(ticket.CheckInDate, checkoutDate);
            ticket.Price = this.ticketCalculator.Calculate(period);
        }

        public Ticket GetTicket(string plate)
        {
            var ticket = tickets.FirstOrDefault(ticket => ticket.Plate == plate);
            if (ticket == null)
            {
                throw new Exception("Ticket not found!");
            }
            return ticket;
        }

        public int GetSlots(int totalSlots)
        {
            return totalSlots - tickets.Count;
        }
    }
}
