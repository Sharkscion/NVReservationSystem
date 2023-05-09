using FlightReservation.Common.Validators;

namespace FlightReservation.Common.Test
{
    public class ReservationValidatorShould
    {
        #region Data Generators
        public static IEnumerable<object[]> GetInvalidFlightDates()
        {
            yield return new object[] { DateTime.Now.AddDays(-1) };
        }

        public static IEnumerable<object[]> GetValidFlightDates()
        {
            yield return new object[] { DateTime.Now };
            yield return new object[] { DateTime.Now.AddDays(1) };
        }
        #endregion

        #region Test Methods
        [Theory]
        [MemberData(nameof(GetValidFlightDates))]
        public void ReturnTrue_WhenValidFlightDate(DateTime value)
        {
            bool result = ReservationValidator.IsFlightDateValid(value);
            Assert.True(result);
        }

        [Theory]
        [MemberData(nameof(GetInvalidFlightDates))]
        public void ReturnFalse_WhenInvalidFlightDate(DateTime value)
        {
            bool result = ReservationValidator.IsFlightDateValid(value);
            Assert.False(result);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("ABC")]
        [InlineData("abcdef")]
        [InlineData("1ABCDE")]
        [InlineData("ABCDEFG")]
        public void ReturnTrue_WhenValidBookingReference(string value)
        {
            bool result = ReservationValidator.IsBookingReferenceFormatValid(value);
            Assert.False(result);
        }

        [Theory]
        [InlineData("ABCDEF")]
        [InlineData("A2CDE3")]
        public void ReturnFalse_WhenInvalidBookingReference(string value)
        {
            bool result = ReservationValidator.IsBookingReferenceFormatValid(value);
            Assert.True(result);
        }
        #endregion
    }
}
