using FlightReservation.Models.Contracts;
using FlightReservation.Services;

namespace FlightReservation.Test.Services.Fixtures
{
    public class FlightServiceFixture : IDisposable
    {
        private readonly FakeFlightRepository _repository;

        public FlightService Service { get; private set; }

        public FlightServiceFixture()
        {
            _repository = new FakeFlightRepository();
            Service = new FlightService(_repository);
        }

        public void Dispose()
        {
            _repository.Dispose();
        }
    }
}
