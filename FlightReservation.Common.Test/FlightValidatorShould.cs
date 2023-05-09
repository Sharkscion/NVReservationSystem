using FlightReservation.Common.Validators;

namespace FlightReservation.Common.Test;

public class FlightValidatorShould
{
    #region Test Methods
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("55")]
    [InlineData("5j")]
    [InlineData("A4CB")]
    public void ReturnFalse_WhenInvalidAirlineCode(string value)
    {
        bool result = FlightValidator.IsAirlineCodeValid(value);
        Assert.False(result);
    }

    [Theory]
    [InlineData("A4C")]
    [InlineData("NV")]
    public void ReturnTrue_WhenValidAirlineCode(string value)
    {
        bool result = FlightValidator.IsAirlineCodeValid(value);
        Assert.True(result);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(10000)]
    public void ValidateFlightNumber(int value)
    {
        bool result = FlightValidator.IsFlightNumberValid(value);
        Assert.False(result);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(123)]
    [InlineData(9999)]
    public void ReturnTrue_WhenValidFlightNumber(int value)
    {
        bool result = FlightValidator.IsFlightNumberValid(value);
        Assert.True(result);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("mnl")]
    [InlineData("MN")]
    [InlineData("1BC")]
    public void ReturnFalse_WhenValidStation(string value)
    {
        bool result = FlightValidator.IsStationFormatValid(value);
        Assert.False(result);
    }

    [Theory]
    [InlineData("MNL")]
    [InlineData("A2C")]
    public void ReturnTrue_WhenValidStation(string value)
    {
        bool result = FlightValidator.IsStationFormatValid(value);
        Assert.True(result);
    }
    #endregion
}
