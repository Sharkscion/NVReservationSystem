using FlightReservation.Models.Flight;

namespace FlightReservation.Test.Models
{
    public class FlightModelShould
    {
        #region Data Generators
        public static IEnumerable<object[]> GetInvalidStations()
        {
            yield return new object[] { null };
            yield return new object[] { "" };
            yield return new object[] { " " };
            yield return new object[] { "abc" };
            yield return new object[] { "AB" };
            yield return new object[] { "1BC" };
        }

        public static IEnumerable<object[]> GetValidStations()
        {
            yield return new object[] { "ABC" };
            yield return new object[] { "A1C" };
            yield return new object[] { "A12" };
        }

        public static IEnumerable<object[]> GetInvalidDepartureArrivalScheduledTimePairs()
        {
            yield return new object[]
            {
                new TimeOnly(hour: 2, minute: 0),
                new TimeOnly(hour: 1, minute: 0)
            };
            yield return new object[]
            {
                new TimeOnly(hour: 2, minute: 0),
                new TimeOnly(hour: 2, minute: 0)
            };
        }
        #endregion

        #region Test Methods
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("55")]
        [InlineData("5j")]
        [InlineData("A4CB")]
        public void RaiseError_WhenInvalidAirlineCode(string value)
        {
            var model = new FlightModel();

            Action action = () => model.AirlineCode = value;

            Assert.Throws<InvalidAirlineCodeException>(action);
        }

        [Theory]
        [InlineData("NV")]
        [InlineData("4AC")]
        public void SetAirlineCode_WhenValid(string value)
        {
            var model = new FlightModel();
            model.AirlineCode = value;

            Assert.Equal(value, model.AirlineCode);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(10000)]
        public void RaiseError_WhenInvalidFlightNumber(int value)
        {
            var model = new FlightModel();

            Action action = () => model.FlightNumber = value;

            Assert.Throws<InvalidFlightNumberException>(action);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(123)]
        [InlineData(9999)]
        public void SetFlightNumber_WhenValid(int value)
        {
            var model = new FlightModel();
            model.FlightNumber = value;

            Assert.Equal(value, model.FlightNumber);
        }

        [Theory]
        [MemberData(nameof(GetInvalidStations))]
        public void RaiseError_WhenInvalidDepartureStation(string value)
        {
            var model = new FlightModel();

            Action action = () => model.DepartureStation = value;

            var exception = Assert.Throws<InvalidStationFormatException>(action);
            Assert.Equal(nameof(model.DepartureStation), exception.ParamName);
        }

        [Fact]
        public void RaiseError_WhenDepartureStationMatchesArrivalStation()
        {
            var model = new FlightModel();
            model.ArrivalStation = "ABC";

            Action action = () => model.DepartureStation = "ABC";

            var exception = Assert.Throws<InvalidMarketPairException>(action);
            Assert.Equal(nameof(model.DepartureStation), exception.ParamName);
        }

        [Theory]
        [MemberData(nameof(GetValidStations))]
        public void SetDepartureStation_WhenValid(string value)
        {
            var model = new FlightModel();

            model.DepartureStation = value;

            Assert.Equal(value, model.DepartureStation);
        }

        [Theory]
        [MemberData(nameof(GetInvalidStations))]
        public void RaiseError_WhenInvalidArrivalStation(string value)
        {
            var model = new FlightModel();

            Action action = () => model.ArrivalStation = value;

            var exception = Assert.Throws<InvalidStationFormatException>(action);
            Assert.Equal(nameof(model.ArrivalStation), exception.ParamName);
        }

        [Fact]
        public void RaiseError_WhenArrivalStationMatchesDepartureStation()
        {
            var model = new FlightModel();
            model.DepartureStation = "ABC";

            Action action = () => model.ArrivalStation = "ABC";

            var exception = Assert.Throws<InvalidMarketPairException>(action);
            Assert.Equal(nameof(model.ArrivalStation), exception.ParamName);
        }

        [Theory]
        [MemberData(nameof(GetValidStations))]
        public void SetArrivalStation_WhenValid(string value)
        {
            var model = new FlightModel();

            model.ArrivalStation = value;

            Assert.Equal(value, model.ArrivalStation);
        }

        [Fact]
        public void SetDepartureScheduledTime_WhenValid()
        {
            var model = new FlightModel();
            model.ArrivalScheduledTime = new TimeOnly(hour: 2, minute: 0);

            model.DepartureScheduledTime = new TimeOnly(hour: 1, minute: 0);

            Assert.Equal( new TimeOnly(hour: 1, minute: 0), model.DepartureScheduledTime);
            Assert.Equal("01:00", model.DepartureScheduledTimeString);
        }

        [Fact]
        public void SetArrivalScheduledTime_WhenValid()
        {
            var model = new FlightModel();
            model.DepartureScheduledTime = new TimeOnly(hour: 11, minute: 0);

            model.ArrivalScheduledTime = new TimeOnly(hour: 10, minute: 0);

            Assert.Equal(new TimeOnly(hour: 10, minute: 0), model.ArrivalScheduledTime);
            Assert.Equal("10:00 +1", model.ArrivalScheduledTimeString);
        }

        [Fact]
        public void DisplayCorrectFlightFormat_WhenSTDPrecedesSTA()
        {
            var model = new FlightModel(
                airlineCode: "AB",
                flightNumber: 1234,
                departureStation: "ABC",
                arrivalStation: "DEF",
                departureScheduledTime: new TimeOnly(hour: 1, minute: 0),
                arrivalScheduledTime: new TimeOnly(hour: 2, minute: 0)
            );

            var result = model.ToString();

            Assert.Equal("AB 1234 ABC->DEF 01:00-02:00", result);
        }

        [Fact]
        public void DisplayCorrectFlightFormat_WhenSTAPrecedesSTD()
        {
            var model = new FlightModel(
                airlineCode: "AB",
                flightNumber: 1234,
                departureStation: "ABC",
                arrivalStation: "DEF",
                departureScheduledTime: new TimeOnly(hour: 11, minute: 30),
                arrivalScheduledTime: new TimeOnly(hour: 10, minute: 30)
            );

            var result = model.ToString();

            Assert.Equal("AB 1234 ABC->DEF 11:30-10:30 +1", result);
        }

        [Fact]
        public void CreateNewInstance_WhenDataSupplied()
        {
            string airlineCode = "NV";
            int flightNumber = 1234;
            string departureStation = "MNL";
            string arrivalStation = "SLC";
            TimeOnly departureScheduledTime = new TimeOnly(hour: 11, minute: 0);
            TimeOnly arrivalScheduledTime = new TimeOnly(hour: 10, minute: 0);

            var initModel = new FlightModel();

            // Action
            var otherModel = initModel.CreateFrom(
                airlineCode,
                flightNumber,
                departureStation,
                arrivalStation,
                departureScheduledTime,
                arrivalScheduledTime
            );

            // Assert
            Assert.Equal("NV 1234 MNL->SLC 11:00-10:00 +1", otherModel.ToString());
            Assert.Equal(departureScheduledTime, otherModel.DepartureScheduledTime);
            Assert.Equal(arrivalScheduledTime, otherModel.ArrivalScheduledTime);
        }
        #endregion
    }
}
