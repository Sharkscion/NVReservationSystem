using System.Text.RegularExpressions;

namespace FlightReservation.Common.Validators
{
    public static class PassengerValidator
    {
        public static bool IsNameValid(string value)
        {
            bool isEmpty = value.Trim() == string.Empty;

            if (isEmpty)
            {
                return false;
            }

            string namePattern = @"^[A-Za-z]{1,20}$";
            if (!Regex.IsMatch(value, namePattern))
            {
                return false;
            }

            return true;
        }

        public static bool IsBirthDateValid(DateTime value)
        {
            bool isAtleast16DaysOld = DateTime.Now.Subtract(value).TotalDays >= 16;

            if (!isAtleast16DaysOld)
            {
                return false;
            }

            return true;
        }
    }
}
