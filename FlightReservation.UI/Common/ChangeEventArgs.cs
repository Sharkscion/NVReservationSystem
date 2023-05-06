namespace FlightReservation.UI.Common
{
    internal class ChangeEventArgs<T> : EventArgs
    {
        public T Value { get; set; }

        public ChangeEventArgs(T value)
        {
            Value = value;
        }
    }
}
