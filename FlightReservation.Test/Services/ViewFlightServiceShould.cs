using FlightReservation.Models.Contracts;
using FlightReservation.Test.Services.Fixtures;

namespace FlightReservation.Test.Services
{
    internal class DateTimeProvider : IDateTimeProvider
    {
        public DateTime GetNow()
        {
            return new DateTime(year: 2023, month: 5, day: 7, hour: 12, minute: 0, second: 0);
        }
    }

    public class ViewFlightServiceShould : IClassFixture<FlightServiceFixture>
    {
        private readonly FlightServiceFixture _fixture;

        public ViewFlightServiceShould(FlightServiceFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void ReturnFlights_HavingAirlineCode()
        {
            string airlineCode = "NV";

            var flights = _fixture.Service.FindAllHaving(airlineCode);

            Assert.Equal(3, flights.Count());
            Assert.Contains(flights, item => item.AirlineCode == airlineCode);
        }

        [Fact]
        public void ReturnFlights_HavingFlightNumber()
        {
            int flightNumber = 1;

            var flights = _fixture.Service.FindAllHaving(flightNumber);

            Assert.Equal(3, flights.Count());
            Assert.Contains(flights, item => item.FlightNumber == flightNumber);
        }

        [Fact]
        public void ReturnFlights_HavingOriginDestination()
        {
            string origin = "MNL";
            string destination = "CEB";

            var flights = _fixture.Service.FindAllHaving(origin, destination);

            Assert.Equal(2, flights.Count());
            Assert.Contains(
                flights,
                item => item.DepartureStation == origin && item.ArrivalStation == destination
            );
        }

        [Fact]
        public void ReturnFlights_HavingFlightDesignator()
        {
            string airlineCode = "NV";
            int flightNumber = 1;

            var flights = _fixture.Service.FindAllHaving(airlineCode, flightNumber);

            Assert.Equal(3, flights.Count());
            Assert.Contains(
                flights,
                item => item.AirlineCode == airlineCode && item.FlightNumber == flightNumber
            );
        }

        [Fact]
        public void ReturnFlights_AvailableToday()
        {
            var dateTimeProvider = new DateTimeProvider();
            DateTime flightDate = dateTimeProvider.GetNow();
            string airlineCode = "NV";
            int flightNumber = 1;

            var flights = _fixture.Service.FindAvailableFlightsOn(
                flightDate,
                airlineCode,
                flightNumber,
                dateTimeProvider
            );

            Assert.Equal(2, flights.Count());
            Assert.Contains(
                flights,
                item =>
                    item.AirlineCode == airlineCode
                    && item.FlightNumber == flightNumber
                    && item.DepartureScheduledTime.Hour > dateTimeProvider.GetNow().Hour
            );
        }
    }
}
