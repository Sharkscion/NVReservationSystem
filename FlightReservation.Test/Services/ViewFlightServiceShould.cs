using FlightReservation.Common.Contracts.Models;
using FlightReservation.Test.Services.Fixtures;

namespace FlightReservation.Test.Services
{
    internal class DateTimeProvider : IDateTimeProvider
    {
        public DateTime GetNow()
        {
            return new DateTime(year: 2023, month: 5, day: 7, hour: 12, minute: 30, second: 0);
        }
    }

    public class ViewFlightServiceShould : IClassFixture<FlightServiceFixture>
    {
        #region Declarations
        private readonly FlightServiceFixture _fixture;

        #endregion

        #region Constructors
        public ViewFlightServiceShould(FlightServiceFixture fixture)
        {
            _fixture = fixture;
        }
        #endregion

        #region Test Methods
        [Fact]
        public void ReturnFlights_WhenFlightMatchAirlineCode()
        {
            string airlineCode = "NV";

            var flights = _fixture.Service.FindAllHaving(airlineCode);

            Assert.Equal(3, flights.Count());
            Assert.Contains(flights, item => item.AirlineCode == airlineCode);
        }

        [Fact]
        public void ReturnFlights_WhenFlightMatchFlightNumber()
        {
            int flightNumber = 1;

            var flights = _fixture.Service.FindAllHaving(flightNumber);

            Assert.Equal(3, flights.Count());
            Assert.Contains(flights, item => item.FlightNumber == flightNumber);
        }

        [Fact]
        public void ReturnFlights_WhenFlightMatchOriginDestination()
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
        public void ReturnFlights_WhenFlightMatchFlightDesignator()
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
        public void ReturnFlights_WhenFlightsAvailableToday()
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

            Assert.Collection(
                flights,
                item =>
                {
                    Assert.Equal(airlineCode, item.AirlineCode);
                    Assert.Equal(flightNumber, item.FlightNumber);
                    Assert.Equal(13, item.DepartureScheduledTime.Hour);
                    Assert.Equal(30, item.DepartureScheduledTime.Minute);
                },
                item =>
                {
                    Assert.Equal(airlineCode, item.AirlineCode);
                    Assert.Equal(flightNumber, item.FlightNumber);
                    Assert.Equal(20, item.DepartureScheduledTime.Hour);
                    Assert.Equal(0, item.DepartureScheduledTime.Minute);
                }
            );
        }
        #endregion
    }
}
