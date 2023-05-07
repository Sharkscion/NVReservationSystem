using FlightReservation.Services;

namespace FlightReservation.Test.Services.Fixtures
{
    public class FlightServiceFixture : IDisposable
    {
        private readonly FakeRepository _repository;

        public FlightService Service { get; private set; }

        public FlightServiceFixture()
        {
            _repository = new FakeRepository();
            Service = new FlightService(_repository);
        }

        public void Dispose()
        {
            _repository.Dispose();
        }
    }
}
