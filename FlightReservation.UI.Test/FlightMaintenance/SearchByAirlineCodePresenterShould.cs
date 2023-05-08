using FlightReservation.Common.Contracts.Models;
using FlightReservation.Common.Contracts.Services;
using FlightReservation.UI.Presenters.FlightMaintenance;
using FlightReservation.UI.Test.Fakes;
using FlightReservation.UI.Views.FlightMaintenance.Contracts;
using Moq;

namespace FlightReservation.UI.Test.FlightMaintenance
{
    public class SearchByAirlineCodePresenterShould : IDisposable
    {
        private readonly Mock<ISearchByAirlineCodeView> _mockView;
        private readonly Mock<IFlightService> _mockService;

        public SearchByAirlineCodePresenterShould()
        {
            _mockView = new Mock<ISearchByAirlineCodeView>();
            _mockService = new Mock<IFlightService>();

            new SearchByAirlineCodePresenter(_mockView.Object, _mockService.Object);
        }

        [Fact]
        public void SetAirlineCodeError_WhenInvalid()
        {
            string airlineCode = "nv";

            _mockView
                .SetupProperty(v => v.AirlineCode, airlineCode)
                .Raise(v => v.AirlineCodeChanged += null, this, EventArgs.Empty);

            _mockView.Verify(
                v =>
                    v.SetFieldError(
                        nameof(v.AirlineCode),
                        "Airline code must be 2-3 uppercased-alphanumeric characters."
                    )
            );

            Assert.False(_mockView.Object.IsFormValid);
        }

        [Fact]
        public void HaveNoAirlineCodeError_WhenValid()
        {
            string airlineCode = "1NV";

            _mockView
                .SetupProperty(v => v.AirlineCode, airlineCode)
                .Raise(v => v.AirlineCodeChanged += null, this, EventArgs.Empty);

            _mockView.Verify(
                v => v.SetFieldError(nameof(v.AirlineCode), It.IsAny<string>()),
                Times.Never()
            );
        }

        [Fact]
        public void Display_AvailableFlights_WhenSubmitted()
        {
            string airlineCode = "1NV";
            int repeatModelCount = 2;
            IEnumerable<IFlight> flights = Enumerable.Repeat(
                new FakeFlightModel(),
                repeatModelCount
            );

            _mockService.Setup(service => service.FindAllHaving(airlineCode)).Returns(flights);
            _mockView.SetupProperty(v => v.AirlineCode, airlineCode);

            _mockView.Raise(v => v.Submitted += null, this, EventArgs.Empty);

            _mockView.Verify(
                v => v.Display(It.Is<IEnumerable<IFlight>>(l => l.Count() == repeatModelCount))
            );
            _mockView.Verify(v => v.Reset());
        }

        [Fact]
        public void Display_NoFlights_WhenSubmitted()
        {
            string airlineCode = "1NV";
            var noFlights = new List<IFlight>();

            _mockView.SetupProperty(v => v.AirlineCode, airlineCode);

            _mockService.Setup(service => service.FindAllHaving(airlineCode)).Returns(noFlights);

            _mockView.Raise(v => v.Submitted += null, this, EventArgs.Empty);

            _mockView.Verify(v => v.DisplayNoFlights());
            _mockView.Verify(v => v.Reset());
        }

        public void Dispose()
        {
            _mockView.Reset();
            _mockService.Reset();
        }
    }
}
