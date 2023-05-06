using FlightReservation.Data.Contracts;

namespace FlightReservation.Services.Contracts
{
    public interface IReservationService
    {
        string Create(IReservation reservation);
        IEnumerable<IReservation> ViewAll();
        IReservation? Find(string PNR);
    }
}
