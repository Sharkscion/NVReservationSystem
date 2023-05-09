using FlightReservation.Common.Contracts.Models;
using FlightReservation.Common.Contracts.Services;
using FlightReservation.UI.Presenters.Reservation;
using FlightReservation.UI.Test.Fakes;
using FlightReservation.UI.Views.Reservation.Contracts;
using Moq;

namespace FlightReservation.UI.Test.Reservation
{
    public class DisplayAllReservationsPresenterShould : IDisposable
    {
        #region Declarations
        private readonly Mock<IDisplayAllReservationsView> _mockView;
        private readonly Mock<IReservationService> _mockService;

        #endregion

        #region Constructors
        public DisplayAllReservationsPresenterShould()
        {
            _mockView = new Mock<IDisplayAllReservationsView>();
            _mockService = new Mock<IReservationService>();

            new DisplayAllReservationsPresenter(_mockView.Object, _mockService.Object);
        }
        #endregion

        #region Test Methods
        [Fact]
        public void DisplayAvailableReservations_WhenSubmitted()
        {
            int repeatCount = 4;
            var availableFlights = Enumerable.Repeat(new FakeReservationModel(), repeatCount);

            _mockService.Setup(s => s.ViewAll()).Returns(availableFlights);

            _mockView.Raise(v => v.Submitted += null, this, EventArgs.Empty);
            _mockView.Verify(
                v =>
                    v.DisplayReservations(
                        It.Is<IEnumerable<IReservation>>(l => l.Count() == repeatCount)
                    )
            );
        }

        [Fact]
        public void DisplayNoReservations_WhenSubmitted()
        {
            var noFlights = new List<IReservation>();

            _mockService.Setup(s => s.ViewAll()).Returns(noFlights);

            _mockView.Raise(v => v.Submitted += null, this, EventArgs.Empty);
            _mockView.Verify(v => v.DisplayNoReservations());
        }
        #endregion

        #region Implementations of IDisposable
        public void Dispose()
        {
            _mockView.Reset();
            _mockService.Reset();
        }
        #endregion
    }
}
