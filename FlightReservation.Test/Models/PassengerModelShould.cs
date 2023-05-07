namespace FlightReservation.Test.Models
{
    public class PassengerModelShould
    {
        public static IEnumerable<object[]> GetInvalidNames()
        {
            yield return new object[] { null};
            yield return new object[] { ""};
            yield return new object[] { " "};
            yield return new object[] { "Name Space"};
            yield return new object[] { "Name1"};
            // More than 20 characters
            yield return new object[] { "abcdefghijklmnopqrstu" };
        }

        public static IEnumerable<object[]> GetValidNames()
        {
            yield return new object[] {"Name"};
            yield return new object[] { "abcdefghijklmnopqrst" };
        }

        [Theory]
        [MemberData(nameof(GetInvalidNames))]
        public void RaiseError_InvalidFirstName(string value)
        {

        }

        [Theory]
        [MemberData(nameof(GetValidNames))]
        public void Set_ValidFirstName(string value)
        {

        }

        [Theory]
        [MemberData(nameof(GetInvalidNames))]
        public void RaiseError_InvalidLastName(string value)
        {

        }

        [Theory]
        [MemberData(nameof(GetValidNames))]
        public void Set_ValidLastName(string value)
        {

        }

        public static IEnumerable<object[]> GetInvalidBirthDates()
        {
            yield return new object[] { DateTime.Now };
            yield return new object[] { DateTime.Now.AddDays(-15) };
        }

        [Theory]
        [MemberData(nameof(GetInvalidBirthDates))]
        public void RaiseError_InvalidBirthDate(DateTime value)
        {

        }

        public static IEnumerable<object[]> GetValidBirthDates()
        {
            yield return new object[] { DateTime.Now.AddDays(-16) };
            yield return new object[] { DateTime.Now.AddYears(-18) };
        }

        [Theory]
        [MemberData(nameof(GetValidBirthDates))]
        public void Set_ValidBirthDate(DateTime value) { }


        public static IEnumerable<object[]> GetBirthDatesAndAgePairs()
        {
            yield return new object[] { DateTime.Now.AddDays(-16),  };
            yield return new object[] { DateTime.Now.AddYears(-18) };
        }

        [Theory]
        [MemberData(nameof(GetBirthDatesAndAgePairs))]
        public void Calculate_CorrectAge(DateTime birthDate, int expectedAge)
        {

        }

    }
}
