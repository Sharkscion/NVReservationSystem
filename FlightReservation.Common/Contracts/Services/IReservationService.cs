
using FlightReservation.Common.Contracts.Models;

namespace FlightReservation.Common.Contracts.Services
{
    public interface IReservationService
    {
        string Create(IReservation reservation);
        IEnumerable<IReservation> ViewAll();
        IReservation? Find(string PNR);
    }
}
