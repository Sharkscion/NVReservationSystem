using FlightReservation.Models.Flight;
using FlightReservation.Test.Services.Fixtures;

namespace FlightReservation.Test.Services
{
    public class CreateFlightServiceShould : IClassFixture<FlightServiceFixture>
    {
        private readonly FlightServiceFixture _fixture;

        public CreateFlightServiceShould(FlightServiceFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void Create_AFlight()
        {
            var model = new FlightModel
            {
                AirlineCode = "NEW",
                FlightNumber = 1,
                DepartureStation = "MNL",
                ArrivalStation = "CEB",
            };

            bool successful = _fixture.Service.Create(model);

            Assert.True(successful);
        }

        [Fact]
        public void ReturnTrue_WhenFlight_Exists()
        {
            var model = new FlightModel
            {
                AirlineCode = "NV",
                FlightNumber = 1,
                DepartureStation = "MNL",
                ArrivalStation = "CEB",
            };

            bool exists = _fixture.Service.DoesExists(model);

            Assert.True(exists);
        }

        [Fact]
        public void ReturnFalse_WhenFlight_DoesNotExists()
        {
            var model = new FlightModel
            {
                AirlineCode = "NO",
                FlightNumber = 2,
                DepartureStation = "MNL",
                ArrivalStation = "CEB",
            };

            bool exists = _fixture.Service.DoesExists(model);

            Assert.False(exists);
        }

        [Fact]
        public void RaiseError_OnDuplicateFlight()
        {
            var existingFlight = new FlightModel
            {
                AirlineCode = "NV",
                FlightNumber = 1,
                DepartureStation = "MNL",
                ArrivalStation = "CEB",
            };

            Action action = () => _fixture.Service.Create(existingFlight);

            Assert.Throws<DuplicateFlightException>(action);
        }
    }
}
