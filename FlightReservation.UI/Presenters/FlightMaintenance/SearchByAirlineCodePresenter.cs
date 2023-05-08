using FlightReservation.Common.Contracts.Models;
using FlightReservation.Common.Contracts.Services;
using FlightReservation.Common.Validators;
using FlightReservation.UI.Presenters.FlightMaintenance.Contracts;
using FlightReservation.UI.Views.FlightMaintenance.Contracts;

namespace FlightReservation.UI.Presenters.FlightMaintenance
{
    internal class SearchByAirlineCodePresenter : ISearchByAirlineCodePresenter
    {
        private readonly ISearchByAirlineCodeView _view;
        private readonly IFlightService _service;

        public SearchByAirlineCodePresenter(ISearchByAirlineCodeView view, IFlightService service)
        {
            _service = service;

            _view = view;
            _view.AirlineCodeChanged += OnAirlineCodeChanged;
            _view.Submitted += OnSubmitted;
        }

        public void OnAirlineCodeChanged(object? source, EventArgs e)
        {
            bool isValid = FlightValidator.IsAirlineCodeValid(value: _view.AirlineCode);
            if (!isValid)
            {
                _view.SetFieldError(
                    nameof(_view.AirlineCode),
                    "Airline code must be 2-3 uppercased-alphanumeric characters."
                );
            }
        }

        public void OnSubmitted(object? source, EventArgs e)
        {
            IEnumerable<IFlight> flights = _service.FindAllHaving(_view.AirlineCode);

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
    }
}
