using FlightReservation.Services;
using FlightReservation.Services.Contracts;

namespace FlightReservation.Test.Services.Fixtures
{
    public class ReservationServiceFixture : IDisposable
    {
        private readonly FakeReservationRepository _reservationRepository;
        private readonly FakeFlightRepository _flightRepository;
        private readonly IFlightService _flightService;

        public IReservationService ReservationService { get; private set; }

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

        public void Dispose()
        {
            _flightRepository.Dispose();
            _reservationRepository.Dispose();
        }
    }
}
