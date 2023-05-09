using FlightReservation.Common.Validators;

namespace FlightReservation.Common.Test
{
    public class PassengerValidatorShould
    {
        #region Data Generators
        public static IEnumerable<object[]> GetValidBirthDates()
        {
            yield return new object[] { DateTime.Now.AddDays(-16) };
            yield return new object[] { DateTime.Now.AddYears(-18) };
        }

        public static IEnumerable<object[]> GetInvalidBirthDates()
        {
            yield return new object[] { DateTime.Now };
            yield return new object[] { DateTime.Now.AddDays(-15) };
        }
        #endregion

        #region Test Methods
        [Theory]
        [MemberData(nameof(GetInvalidBirthDates))]
        public void ReturnFalse_WhenInvalidBirthDate(DateTime value)
        {
            bool result = PassengerValidator.IsBirthDateValid(value);
            Assert.False(result);
        }

        [Theory]
        [MemberData(nameof(GetValidBirthDates))]
        public void ReturnTrue_WhenValidBirthDate(DateTime value)
        {
            bool result = PassengerValidator.IsBirthDateValid(value);
            Assert.True(result);
        }

        [Theory]
        [InlineData("Name")]
        [InlineData("abcdefghijklmnopqrst")]
        public void ReturnTrue_WhenValidName(string value)
        {
            bool result = PassengerValidator.IsNameValid(value);
            Assert.True(result);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("Name Space")]
        [InlineData("Name1")]
        // More than 20 characters
        [InlineData("abcdefghijklmnopqrstu")]
        public void ReturnFalse_WhenInvalidName(string value)
        {
            bool result = PassengerValidator.IsNameValid(value);
            Assert.False(result);
        }
        #endregion
    }
}
