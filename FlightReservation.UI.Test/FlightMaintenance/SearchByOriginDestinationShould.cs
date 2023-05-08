using FlightReservation.Models.Contracts;
using FlightReservation.Services.Contracts;
using FlightReservation.UI.Presenters.FlightMaintenance;
using FlightReservation.UI.Test.Fakes;
using FlightReservation.UI.Views.FlightMaintenance.Contracts;
using Moq;

namespace FlightReservation.UI.Test.FlightMaintenance
{
    public class SearchByOriginDestinationShould : IDisposable
    {
        private readonly Mock<ISearchByOriginDestinationView> _mockView;
        private readonly Mock<IFlightService> _mockService;

        public SearchByOriginDestinationShould()
        {
            _mockView = new Mock<ISearchByOriginDestinationView>();
            _mockService = new Mock<IFlightService>();

            new SearchByOriginDestinationPresenter(_mockView.Object, _mockService.Object);
        }

        [Fact]
        public void SetOriginError_WhenInvalidFormat()
        {
            _mockView
                .SetupProperty(v => v.DepartureStation, "1NV")
                .Raise(v => v.DepartureStationChanged += null, this, EventArgs.Empty);

            _mockView.Verify(
                v =>
                    v.SetFieldError(
                        nameof(v.DepartureStation),
                        "Departure station code must be 3 uppercased-alphanumeric characters."
                    )
            );

            Assert.False(_mockView.Object.IsFormValid);
        }

        [Fact]
        public void SetOriginError_WhenMatchedWithArrivalStation()
        {
            _mockView.SetupProperty(v => v.ArrivalStation, "MNL");
            _mockView
                .SetupProperty(v => v.DepartureStation, "MNL")
                .Raise(v => v.DepartureStationChanged += null, this, EventArgs.Empty);

            _mockView.Verify(
                v =>
                    v.SetFieldError(
                        nameof(v.DepartureStation),
                        "Departure station must not be the same as the arrival station."
                    )
            );

            Assert.False(_mockView.Object.IsFormValid);
        }

        [Fact]
        public void HaveNoOriginError_WhenValid()
        {
            _mockView.SetupProperty(v => v.DepartureStation, "MNL");

            _mockView.Verify(
                v => v.SetFieldError(nameof(v.DepartureStation), It.IsAny<string>()),
                Times.Never
            );
        }

        [Fact]
        public void SetDestinationError_WhenInvalidFormat()
        {
            _mockView
                .SetupProperty(v => v.ArrivalStation, "1NV")
                .Raise(v => v.ArrivalStationChanged += null, this, EventArgs.Empty);

            _mockView.Verify(
                v =>
                    v.SetFieldError(
                        nameof(v.ArrivalStation),
                        "Arrival station code must be 3 uppercased-alphanumeric characters."
                    )
            );

            Assert.False(_mockView.Object.IsFormValid);
        }

        [Fact]
        public void SetDestinationError_WhenMatchedWithDepartureStation()
        {
            _mockView.SetupProperty(v => v.DepartureStation, "MNL");
            _mockView
                .SetupProperty(v => v.ArrivalStation, "MNL")
                .Raise(v => v.ArrivalStationChanged += null, this, EventArgs.Empty);

            _mockView.Verify(
                v =>
                    v.SetFieldError(
                        nameof(v.ArrivalStation),
                        "Arrival station must not be the same as the departure station."
                    )
            );

            Assert.False(_mockView.Object.IsFormValid);
        }

        [Fact]
        public void HaveNoDestinationError_WhenValid()
        {
            _mockView
                .SetupProperty(v => v.ArrivalStation, "MNL")
                .Raise(v => v.ArrivalStationChanged += null, this, EventArgs.Empty);

            _mockView.Verify(
                v => v.SetFieldError(nameof(v.ArrivalStation), It.IsAny<string>()),
                Times.Never
            );
        }

        [Fact]
        public void Display_AvailableFlights_WhenSubmitted()
        {
            string origin = "MNL";
            string destination = "CEB";

            int repeatModelCount = 2;
            IEnumerable<IFlight> flights = Enumerable.Repeat(
                new FakeFlightModel(),
                repeatModelCount
            );

            _mockService
                .Setup(service => service.FindAllHaving(origin, destination))
                .Returns(flights);
            _mockView.SetupProperty(v => v.DepartureStation, origin);
            _mockView.SetupProperty(v => v.ArrivalStation, destination);

            _mockView.Raise(v => v.Submitted += null, this, EventArgs.Empty);

            _mockView.Verify(
                v => v.Display(It.Is<IEnumerable<IFlight>>(l => l.Count() == repeatModelCount))
            );
            _mockView.Verify(v => v.Reset());
        }

        [Fact]
        public void Display_NoFlights_WhenSubmitted()
        {
            string origin = "MNL";
            string destination = "CEB";

            var noFlights = new List<IFlight>();

            _mockService
                .Setup(service => service.FindAllHaving(origin, destination))
                .Returns(noFlights);
            _mockView.SetupProperty(v => v.DepartureStation, origin);
            _mockView.SetupProperty(v => v.ArrivalStation, destination);

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
