using FlightReservation.UI.Common;

namespace FlightReservation.UI.Views.Contracts
{
    internal interface IFormView
    {
        delegate void InputChangedEventHandler<T>(IFormView sender, ChangeEventArgs<T> args);
        delegate void SubmitEventHandler<T>(IFormView sender, T args);
        void Reset();
    }
}
