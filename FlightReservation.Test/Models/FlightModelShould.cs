using FlightReservation.Data.Flight;

namespace FlightReservation.Test.Models
{
    public class FlightModelShould
    {
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

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("55")]
        [InlineData("5j")]
        [InlineData("A4CB")]
        public void RaiseError_InvalidAirlineCode(string value)
        {
            var model = new FlightModel();

            Action action = () => model.AirlineCode = value;

            Assert.Throws<InvalidAirlineCodeException>(action);
        }

        [Theory]
        [InlineData("NV")]
        [InlineData("4AC")]
        public void Set_ValidAirlineCode(string value)
        {
            var model = new FlightModel();
            model.AirlineCode = value;

            Assert.Equal(value, model.AirlineCode);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(10000)]
        public void RaiseError_InvalidFlightNumber(int value)
        {
            var model = new FlightModel();

            Action action = () => model.FlightNumber = value;

            Assert.Throws<InvalidFlightNumberException>(action);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(123)]
        [InlineData(9999)]
        public void Set_ValidFlightNumber(int value)
        {
            var model = new FlightModel();
            model.FlightNumber = value;

            Assert.Equal(value, model.FlightNumber);
        }

        [Theory]
        [MemberData(nameof(GetInvalidStations))]
        public void RaiseError_InvalidDepartureStation(string value)
        {
            var model = new FlightModel();

            Action action = () => model.DepartureStation = value;

            var exception = Assert.Throws<InvalidStationFormatException>(action);
            Assert.Equal(nameof(model.DepartureStation), exception.ParamName);
        }

        [Fact]
        public void RaiseError_DepartureStationMatchesArrivalStation()
        {
            var model = new FlightModel();
            model.ArrivalStation = "ABC";

            Action action = () => model.DepartureStation = "ABC";

            var exception = Assert.Throws<InvalidMarketPairException>(action);
            Assert.Equal(nameof(model.DepartureStation), exception.ParamName);
        }

        [Theory]
        [MemberData(nameof(GetValidStations))]
        public void Set_ValidDepartureStation(string value)
        {
            var model = new FlightModel();

            model.DepartureStation = value;

            Assert.Equal(value, model.DepartureStation);
        }

        [Theory]
        [MemberData(nameof(GetInvalidStations))]
        public void RaiseError_InvalidArrivalStation(string value)
        {
            var model = new FlightModel();

            Action action = () => model.ArrivalStation = value;

            var exception = Assert.Throws<InvalidStationFormatException>(action);
            Assert.Equal(nameof(model.ArrivalStation), exception.ParamName);
        }

        [Fact]
        public void RaiseError_ArrivalStationMatchesDepartureStation()
        {
            var model = new FlightModel();
            model.DepartureStation = "ABC";

            Action action = () => model.ArrivalStation = "ABC";

            var exception = Assert.Throws<InvalidMarketPairException>(action);
            Assert.Equal(nameof(model.ArrivalStation), exception.ParamName);
        }

        [Theory]
        [MemberData(nameof(GetValidStations))]
        public void Set_ValidArrivalStation(string value)
        {
            var model = new FlightModel();

            model.ArrivalStation = value;

            Assert.Equal(value, model.ArrivalStation);
        }

        [Theory]
        [MemberData(nameof(GetInvalidDepartureArrivalScheduledTimePairs))]
        public void RaiseError_InvalidDepartureScheduledTime(
            TimeOnly departureScheduledTime,
            TimeOnly arrivalScheduledTime
        )
        {
            var model = new FlightModel();
            model.ArrivalScheduledTime = arrivalScheduledTime;

            Action action = () => model.DepartureScheduledTime = departureScheduledTime;

            var exception = Assert.Throws<InvalidFlightTimeException>(action);
            Assert.Equal(nameof(model.DepartureScheduledTime), exception.ParamName);
        }

        [Fact]
        public void Set_ValidDepartureScheduledTime()
        {
            var model = new FlightModel();
            model.ArrivalScheduledTime = new TimeOnly(hour: 2, minute: 0);

            model.DepartureScheduledTime = new TimeOnly(hour: 1, minute: 0);

            Assert.Equal(model.DepartureScheduledTime, new TimeOnly(hour: 1, minute: 0));
        }

        [Theory]
        [MemberData(nameof(GetInvalidDepartureArrivalScheduledTimePairs))]
        public void RaiseError_InvalidArrivalScheduledTime(
            TimeOnly departureScheduledTime,
            TimeOnly arrivalScheduledTime
        )
        {
            var model = new FlightModel();
            model.DepartureScheduledTime = departureScheduledTime;

            Action action = () => model.ArrivalScheduledTime = arrivalScheduledTime;

            var exception = Assert.Throws<InvalidFlightTimeException>(action);
            Assert.Equal(nameof(model.ArrivalScheduledTime), exception.ParamName);
        }

        [Fact]
        public void Set_ValidArrivalScheduledTime()
        {
            var model = new FlightModel();
            model.DepartureScheduledTime = new TimeOnly(hour: 1, minute: 0);

            model.ArrivalScheduledTime = new TimeOnly(hour: 2, minute: 0);

            Assert.Equal(model.ArrivalScheduledTime, new TimeOnly(hour: 2, minute: 0));
        }

        [Fact]
        public void Display_CorrectFlightFormat()
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

            Assert.Equal("AB 1234 ABC->DEF", result);
        }
    }
}
