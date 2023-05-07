using FlightReservation.Common.Validators;

namespace FlightReservation.Common.Test
{
    public class ReservationValidatorShould
    {
        public static IEnumerable<object[]> GetFlightDates()
        {
            yield return new object[] { DateTime.Now.AddDays(-1), false };
            yield return new object[] { DateTime.Now, true };
            yield return new object[] { DateTime.Now.AddDays(1), true };
        }

        [Theory]
        [MemberData(nameof(GetFlightDates))]
        public void ValidateFlightDate(DateTime value, bool expectedResult)
        {
            bool result = ReservationValidator.IsFlightDateValid(value);
            Assert.Equal(expectedResult, result);
        }

        [Theory]
        [InlineData(null, false)]
        [InlineData("ABC", false)]
        [InlineData("abcdef", false)]
        [InlineData("1ABCDE", false)]
        [InlineData("ABCDEFG", false)]
        [InlineData("ABCDEF", true)]
        [InlineData("A2CDE3", true)]
        public void ValidateBookingReference(string value, bool expectedResult)
        {
            bool result = ReservationValidator.IsBookingReferenceFormatValid(value);
            Assert.Equal(expectedResult, result);
        }
    }
}
