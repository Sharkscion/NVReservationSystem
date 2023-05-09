using FlightReservation.Common.Contracts.Models;
using FlightReservation.Common.Contracts.Services;
using FlightReservation.Common.Validators;
using FlightReservation.UI.Presenters.FlightMaintenance.Contracts;
using FlightReservation.UI.Views.FlightMaintenance.Contracts;

namespace FlightReservation.UI.Presenters.FlightMaintenance
{
    internal class SearchByFlightNumberPresenter : ISearchByFlightNumberPresenter
    {
        #region Declarations
        private readonly ISearchByFlightNumberView _view;
        private readonly IFlightService _service;
        #endregion

        #region Constructors
        public SearchByFlightNumberPresenter(ISearchByFlightNumberView view, IFlightService service)
        {
            _service = service;

            _view = view;
            _view.FlightNumberChanged += OnFlightNumberChanged;
            _view.Submitted += OnSubmitted;
        }
        #endregion

        #region Implementations of ISearchByFlightNumberPresenter
        public void OnFlightNumberChanged(object? source, EventArgs e)
        {
            bool isValid = FlightValidator.IsFlightNumberValid(_view.FlightNumber);
            if (!isValid)
            {
                _view.SetFieldError(
                    nameof(_view.FlightNumber),
                    "Flight number must be an integer between 1 and 9999."
                );
            }
        }

        public void OnSubmitted(object? source, EventArgs e)
        {
            IEnumerable<IFlight> flights = _service.FindAllHaving(_view.FlightNumber);

            if (flights.Count() > 0)
            {
                _view.Display(flights);
            }
            else
            {
                _view.DisplayNoFlights();
            }

            _view.Reset();
        }
        #endregion
    }
}
