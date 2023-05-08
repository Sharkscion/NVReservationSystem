using FlightReservation.Common.Contracts.Models;
using FlightReservation.Common.Contracts.Services;
using FlightReservation.UI.Presenters.FlightMaintenance;
using FlightReservation.UI.Test.Fakes;
using FlightReservation.UI.Views.FlightMaintenance.Contracts;
using Moq;

namespace FlightReservation.UI.Test.FlightMaintenance
{
    public class SearchByFlightNumberPresenterShould : IDisposable
    {
        private readonly Mock<ISearchByFlightNumberView> _mockView;
        private readonly Mock<IFlightService> _mockService;

        public SearchByFlightNumberPresenterShould()
        {
            _mockView = new Mock<ISearchByFlightNumberView>();
            _mockService = new Mock<IFlightService>();

            new SearchByFlightNumberPresenter(_mockView.Object, _mockService.Object);
        }

        [Fact]
        public void SetFlightNumberError_WhenInvalid()
        {
            _mockView
                .SetupProperty(v => v.FlightNumber, -1)
                .Raise(v => v.FlightNumberChanged += null, this, EventArgs.Empty);

            _mockView.Verify(
                v =>
                    v.SetFieldError(
                        nameof(v.FlightNumber),
                        "Flight number must be an integer between 1 and 9999."
                    )
            );

            Assert.False(_mockView.Object.IsFormValid);
        }

        [Fact]
        public void HaveNoFlightNumberError_WhenValid()
        {
            _mockView
                .SetupProperty(v => v.FlightNumber, 9999)
                .Raise(v => v.FlightNumberChanged += null, this, EventArgs.Empty);

            _mockView.Verify(
                v => v.SetFieldError(nameof(v.FlightNumber), It.IsAny<string>()),
                Times.Never()
            );
        }

        [Fact]
        public void Display_AvailableFlights_WhenSubmitted()
        {
            int flightNumber = 9999;
            int repeatModelCount = 2;
            IEnumerable<IFlight> flights = Enumerable.Repeat(
                new FakeFlightModel(),
                repeatModelCount
            );

            _mockService.Setup(service => service.FindAllHaving(flightNumber)).Returns(flights);
            _mockView.SetupProperty(v => v.FlightNumber, flightNumber);

            _mockView.Raise(v => v.Submitted += null, this, EventArgs.Empty);

            _mockView.Verify(
                v => v.Display(It.Is<IEnumerable<IFlight>>(l => l.Count() == repeatModelCount))
            );
            _mockView.Verify(v => v.Reset());
        }

        [Fact]
        public void Display_NoFlights_WhenSubmitted()
        {
            int flightNumber = 1;
            var noFlights = new List<IFlight>();

            _mockView.SetupProperty(v => v.FlightNumber, flightNumber);

            _mockService.Setup(service => service.FindAllHaving(flightNumber)).Returns(noFlights);

            _mockView.Raise(v => v.Submitted += null, this, EventArgs.Empty);

            _mockView.Verify(v => v.DisplayNoFlights());
            _mockView.Verify(v => v.Reset());
        }

        public void Dispose()
        {
            _mockService.Reset();
            _mockView.Reset();
        }
    }
}
