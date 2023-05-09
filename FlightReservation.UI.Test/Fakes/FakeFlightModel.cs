using FlightReservation.Common.Contracts.Models;

namespace FlightReservation.UI.Test.Fakes
{
    internal class FakeFlightModel : IFlight
    {
        #region Properties
        public string AirlineCode { get; set; }
        public int FlightNumber { get; set; }
        public string ArrivalStation { get; set; }
        public string DepartureStation { get; set; }
        public TimeOnly ArrivalScheduledTime { get; set; }
        public TimeOnly DepartureScheduledTime { get; set; }
        #endregion

        #region Constructors
        public FakeFlightModel() { }

        public FakeFlightModel(
            string airlineCode,
            int flightNumber,
            string arrivalStation,
            string departureStation,
            TimeOnly arrivalScheduledTime,
            TimeOnly departureScheduledTime
        )
        {
            AirlineCode = airlineCode;
            FlightNumber = flightNumber;
            ArrivalStation = arrivalStation;
            DepartureStation = departureStation;
            ArrivalScheduledTime = arrivalScheduledTime;
            DepartureScheduledTime = departureScheduledTime;
        }
        #endregion

        #region Public Methods
        public IFlight CreateFrom(
            string airlineCode,
            int flightNumber,
            string departureStation,
            string arrivalStation,
            TimeOnly departureScheduledTime,
            TimeOnly arrivalScheduledTime
        )
        {
            return new FakeFlightModel(
                airlineCode,
                flightNumber,
                arrivalStation,
                departureStation,
                arrivalScheduledTime,
                departureScheduledTime
            );
        }
        #endregion
    }
}
