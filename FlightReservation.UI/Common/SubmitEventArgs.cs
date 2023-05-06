namespace FlightReservation.UI.Common
{
    internal class SubmitEventArgs<T> : EventArgs
    {
        public T Data { get; set; }

        public SubmitEventArgs(T data)
        {
            Data = data;
        }
    }
}
