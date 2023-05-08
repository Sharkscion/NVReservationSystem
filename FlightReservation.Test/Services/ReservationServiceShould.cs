using FlightReservation.Common.Contracts.Models;
using FlightReservation.Models.Flight;
using FlightReservation.Models.Passenger;
using FlightReservation.Models.Reservation;
using FlightReservation.Test.Services.Fixtures;

namespace FlightReservation.Test.Services
{
    public class ReservationServiceShould : IClassFixture<ReservationServiceFixture>
    {
        private readonly ReservationServiceFixture _fixture;

        public ReservationServiceShould(ReservationServiceFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void Create_AReservation()
        {
            var flight = new FlightModel
            {
                AirlineCode = "NV",
                FlightNumber = 1,
                DepartureStation = "MNL",
                ArrivalStation = "CEB",
                DepartureScheduledTime = new TimeOnly(hour: 8, minute: 0),
                ArrivalScheduledTime = new TimeOnly(hour: 9, minute: 0)
            };

            var passengers = new List<PassengerModel>
            {
                new PassengerModel("John", "Doe", new DateTime(1990, 1, 1)),
                new PassengerModel("Mary", "Poppins", new DateTime(1990, 1, 1)),
            };

            var reservation = new ReservationModel(
                flightInfo: flight,
                flightDate: DateTime.Now.AddDays(1),
                passengers: passengers
            );

            string PNR = _fixture.ReservationService.Create(reservation);

            Assert.Matches(@"^[A-Z][A-Z0-9]{5}$", PNR);
        }

        [Fact]
        public void RaiseError_WhenBookedFlight_DoesNotExists()
        {
            var noneExistingFlight = new FlightModel
            {
                AirlineCode = "NV",
                FlightNumber = 1,
                DepartureStation = "ABC",
                ArrivalStation = "DEF",
                DepartureScheduledTime = new TimeOnly(hour: 8, minute: 0),
                ArrivalScheduledTime = new TimeOnly(hour: 9, minute: 0)
            };

            var passengers = new List<PassengerModel>
            {
                new PassengerModel("John", "Doe", new DateTime(1990, 1, 1)),
                new PassengerModel("Mary", "Poppins", new DateTime(1990, 1, 1)),
            };

            var reservation = new ReservationModel(
                flightInfo: noneExistingFlight,
                flightDate: DateTime.Now.AddDays(1),
                passengers: passengers
            );

            Action action = () => _fixture.ReservationService.Create(reservation);

            Assert.Throws<FlightDoesNotExistException>(action);
        }

        [Fact]
        public void Return_AllManagedReservations()
        {
            IEnumerable<IReservation> reservations = _fixture.ReservationService.ViewAll();

            Assert.NotEmpty(reservations);
            Assert.Contains(reservations, item => item.PNR == "ABC123");
        }

        [Fact]
        public void ReturnReservation_MatchingPNR()
        {
            IReservation? reservation = _fixture.ReservationService.Find("ABC123");

            Assert.NotNull(reservation);
            Assert.Equal("ABC123", reservation.PNR);
        }

        [Fact]
        public void ReturnNull_NoMatchingPNR()
        {
            IReservation? reservation = _fixture.ReservationService.Find("NONE");

            Assert.Null(reservation);
        }
    }
}
