using FlightReservation.Common.Validators;

namespace FlightReservation.Common.Test
{
    public class PassengerValidatorShould
    {
        public static IEnumerable<object[]> GetBirthDates()
        {
            yield return new object[] { DateTime.Now, false };
            yield return new object[] { DateTime.Now.AddDays(-15), false };
            yield return new object[] { DateTime.Now.AddDays(-16), true };
            yield return new object[] { DateTime.Now.AddYears(-18), true };
        }

        [Theory]
        [MemberData(nameof(GetBirthDates))]
        public void ValidateBirthDate(DateTime value, bool expectedResult)
        {
            bool result = PassengerValidator.IsBirthDateValid(value);
            Assert.Equal(expectedResult, result);
        }

        [Theory]
        [InlineData(null, false)]
        [InlineData("", false)]
        [InlineData(" ", false)]
        [InlineData("Name Space", false)]
        [InlineData("Name1", false)]
        [InlineData("Name", true)]
        [InlineData("abcdefghijklmnopqrst", true)]
        // More than 20 characters
        [InlineData("abcdefghijklmnopqrstu", false)]
        public void ValidateName(string value, bool expectedResult)
        {
            bool result = PassengerValidator.IsNameValid(value);
            Assert.Equal(expectedResult, result);
        }
    }
}
