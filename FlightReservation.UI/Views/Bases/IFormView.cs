using FlightReservation.UI.Common;

namespace FlightReservation.UI.Views.Contracts
{
    internal interface IFormView
    {
        public bool IsFormValid { get; }

        void SetFieldError(string paramName, string message);
        void AlertError(string header, string message);
        void Reset();
    }
}
