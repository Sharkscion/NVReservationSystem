using FlightReservation.Common.Contracts.Services;
using FlightReservation.Services;

namespace FlightReservation.Test.Services.Fixtures
{
    public class ReservationServiceFixture : IDisposable
    {
        #region Declarations
        private readonly FakeReservationRepository _reservationRepository;
        private readonly FakeFlightRepository _flightRepository;
        private readonly IFlightService _flightService;
        #endregion

        #region Properties
        public IReservationService ReservationService { get; private set; }

        #endregion

        #region Constructors
        public ReservationServiceFixture()
        {
            _flightRepository = new FakeFlightRepository();
            _flightService = new FlightService(_flightRepository);

            _reservationRepository = new FakeReservationRepository();
            ReservationService = new ReservationService(
                reservationRepository: _reservationRepository,
                flightService: _flightService
            );
        }
        #endregion

        #region Implementations of IDisposable
        public void Dispose()
        {
            _flightRepository.Dispose();
            _reservationRepository.Dispose();
        }
        #endregion
    }
}
