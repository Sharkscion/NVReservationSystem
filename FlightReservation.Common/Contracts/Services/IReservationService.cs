using FlightReservation.Common.Contracts.Models;

namespace FlightReservation.Common.Contracts.Services
{
    /// <summary>
    /// Contract for managing business logics related to a reservation entity.
    /// </summary>
    public interface IReservationService
    {
        #region Functions
        string Create(IReservation reservation);
        IEnumerable<IReservation> ViewAll();
        IReservation? Find(string PNR);
        #endregion
    }
}
