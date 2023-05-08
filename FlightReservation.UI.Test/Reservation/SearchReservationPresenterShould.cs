using FlightReservation.Models.Contracts;
using FlightReservation.Services.Contracts;
using FlightReservation.UI.Presenters.Reservation;
using FlightReservation.UI.Test.Fakes;
using FlightReservation.UI.Views.Reservation.Contracts;
using Moq;

namespace FlightReservation.UI.Test.Reservation
{
    public class SearchReservationPresenterShould : IDisposable
    {
        private readonly Mock<ISearchReservationView> _mockView;
        private readonly Mock<IReservationService> _mockService;

        public SearchReservationPresenterShould()
        {
            _mockView = new Mock<ISearchReservationView>();
            _mockService = new Mock<IReservationService>();

            new SearchReservationPresenter(_mockView.Object, _mockService.Object);
        }

        [Fact]
        public void SetPNRError_WhenInvalid()
        {
            _mockView
                .SetupProperty(v => v.PNR, "Invalid")
                .Raise(v => v.PNRChanged += null, this, EventArgs.Empty);

            _mockView.Verify(
                v =>
                    v.SetFieldError(
                        nameof(v.PNR),
                        "PNR should be 6 uppercased-alphanumeric characters."
                    )
            );

            Assert.False(_mockView.Object.IsFormValid);
        }

        [Fact]
        public void Display_AvailableReservation_WhenSubmitted()
        {
            string PNR = "ABC123";
            _mockView.SetupProperty(v => v.PNR, PNR);

            _mockService.Setup(s => s.Find(PNR)).Returns(new FakeReservationModel("PNR"));

            _mockView.Raise(v => v.Submitted += null, this, EventArgs.Empty);
            _mockView.Verify(v => v.DisplayReservation(It.IsAny<IReservation>()));
            _mockView.Verify(v => v.Reset());
        }

        [Fact]
        public void Display_NoReservation_WhenSubmitted()
        {
            string PNR = "NOPNR1";
            _mockView.SetupProperty(v => v.PNR, PNR);

            _mockService.Setup(s => s.Find(PNR)).Returns<IReservation?>(null);

            _mockView.Raise(v => v.Submitted += null, this, EventArgs.Empty);
            _mockView.Verify(v => v.DisplayNoReservation());
            _mockView.Verify(v => v.Reset());
        }

        public void Dispose()
        {
            _mockView.Reset();
            _mockService.Reset();
        }
    }
}
