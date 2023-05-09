using FlightReservation.Common.Contracts.Services;
using FlightReservation.Services;

namespace FlightReservation.Test.Services.Fixtures
{
    public class FlightServiceFixture : IDisposable
    {
        #region Declarations
        private readonly FakeFlightRepository _repository;

        #endregion

        #region Properties
        public IFlightService Service { get; private set; }

        #endregion

        #region Constructors
        public FlightServiceFixture()
        {
            _repository = new FakeFlightRepository();
            Service = new FlightService(_repository);
        }
        #endregion

        #region Implementation of IDisposable
        public void Dispose()
        {
            _repository.Dispose();
        }
        #endregion
    }
}
