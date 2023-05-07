using FlightReservation.Common.Validators;

namespace FlightReservation.Common.Test;

public class FlightValidatorShould
{
    [Theory]
    [InlineData(null, false)]
    [InlineData("", false)]
    [InlineData(" ", false)]
    [InlineData("55", false)]
    [InlineData("5j", false)]
    [InlineData("A4CB", false)]
    [InlineData("A4C", true)]
    [InlineData("NV", true)]
    public void ValidateAirlineCode(string value, bool expectedResult)
    {
        bool result = FlightValidator.IsAirlineCodeValid(value);
        Assert.Equal(expectedResult, result);
    }

    [Theory]
    [InlineData(0, false)]
    [InlineData(1, true)]
    [InlineData(123, true)]
    [InlineData(9999, true)]
    [InlineData(10000, false)]
    public void ValidateFlightNumber(int value, bool expectedResult)
    {
        bool result = FlightValidator.IsFlightNumberValid(value);
        Assert.Equal(expectedResult, result);
    }

    [Theory]
    [InlineData(null, false)]
    [InlineData("", false)]
    [InlineData(" ", false)]
    [InlineData("mnl", false)]
    [InlineData("MN", false)]
    [InlineData("1BC", false)]
    [InlineData("MNL", true)]
    [InlineData("A2C", true)]
    public void ValidateStation(string value, bool expectedResult)
    {
        bool result = FlightValidator.IsStationFormatValid(value);
        Assert.Equal(expectedResult, result);
    }
}
