using DesignPatterns.Behavioral.Strategy_Parking;
using FluentAssertions;

namespace DesignPatterns.Tests.Strategy
{
    public class ParkingLotTests
    {
        [Test]
        public void ShouldBeAbleToCreateAParkingLot()
        {
            var parkingLot = new ParkingLot("airport", 500);
            parkingLot.GetSlots().Should().Be(500);
        }

        [Test]
        public void ShouldBeAble_ToParkTheCarOnTheBeachForTwoHours_AndWhenLeavingTheValueShouldBeTen_FiveByHour()
        {
            var parkingLot = new ParkingLot("beach", 500);
            var ticketDto = new Ticket
            {
                CheckInDate = new DateTime(2021, 10, 01, 10, 00, 00),
                Plate = "AAA-1111",
            };
            
            parkingLot.CheckIn(ticketDto);
            parkingLot.CheckOut("AAA-1111", new DateTime(2021, 10, 01, 12, 00, 00));

            var ticket = parkingLot.GetTicket("AAA-1111");
            ticket.Price.Should().Be(10);
        }

        [Test]
        public void ShouldBeAble_ToParkTheCarInTheShoppingForSevenHours_AndWhenLeavingTheValueShouldBeTwentyTwo_TenTheFirstThreeHoursAndAfterThreeForHour()
        {
            var parkingLot = new ParkingLot("shopping", 500);
            var ticketDto = new Ticket
            {
                CheckInDate = new DateTime(2021, 10, 01, 10, 00, 00),
                Plate = "AAA-1111",
            };

            parkingLot.CheckIn(ticketDto);
            parkingLot.CheckOut("AAA-1111", new DateTime(2021, 10, 01, 17, 00, 00));

            var ticket = parkingLot.GetTicket("AAA-1111");
            ticket.Price.Should().Be(22);        
        }

        [Test]
        public void ShouldBeAble_ToParkInTheAirportForThreeDays_AndWhenLeavingTheValueShouldBeOneHundredAndFifty_FiftyForDay()
        {
            var parkingLot = new ParkingLot("airport", 500);
            var ticketDto = new Ticket
            {
                CheckInDate = new DateTime(2021, 10, 01, 10, 00, 00),
                Plate = "AAA-1111",
            };

            parkingLot.CheckIn(ticketDto);
            parkingLot.CheckOut("AAA-1111", new DateTime(2021, 10, 04, 10, 00, 00));

            var ticket = parkingLot.GetTicket("AAA-1111");
            ticket.Price.Should().Be(150);
        }
    }
}
