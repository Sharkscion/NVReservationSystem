using System.Text.RegularExpressions;

namespace FlightReservation.Common.Validators
{
    public static class ReservationValidator
    {

        public static bool IsFlightDateValid(DateTime value)
        {
            bool isOnOrAfterToday = value.Date >= DateTime.Now.Date;
            if (!isOnOrAfterToday)
            {
                return false;
            }

            return true;
        }

        public static bool IsBookingReferenceFormatValid(string value)
        {
            string pattern = @"^[A-Z][A-Z0-9]{5}$";
            if (!Regex.IsMatch(value, pattern))
            {
                return false;
            }

            return true;
        }
    }
}
