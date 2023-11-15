namespace DesignPatterns.Behavioral.Strategy_Parking
{
    public class ParkingLot
    {
        private readonly IList<Ticket> _tickets;
        private readonly ITicketCalculator _ticketCalculator;
        private readonly int _totalLots;

        public ParkingLot(string location, int totalLots)
        {
            _tickets = new List<Ticket>();
            _ticketCalculator = TicketCalculatorFactory.Create(location);
            _totalLots = totalLots;
        }

        public void CheckIn(Ticket ticket)
        {
            _tickets.Add(ticket);
        }

        public void CheckOut(string plate, DateTime checkoutDate)
        {
            var ticket = GetTicket(plate);
            var period = new Period(ticket.CheckInDate, checkoutDate);
            ticket.Price = _ticketCalculator.Calculate(period);
        }

        public Ticket GetTicket(string plate)
        {
            var ticket = _tickets.FirstOrDefault(ticket => ticket.Plate == plate);

            if (ticket == null)
            {
                throw new Exception("Ticket not found!");
            }
            return ticket;
        }

        public int GetSlots()
        {
            return _totalLots - _tickets.Count;
        }
    }
}
