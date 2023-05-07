using FlightReservation.Common.Validators;
using FlightReservation.Services.Contracts;
using FlightReservation.UI.Common;
using FlightReservation.UI.Presenters.FlightMaintenance.Contracts;
using FlightReservation.UI.Views.Contracts;
using FlightReservation.UI.Views.FlightMaintenance.Contracts;

namespace FlightReservation.UI.Presenters.FlightMaintenance
{
    internal class SearchByAirlineCodePresenter : ISearchByAirlineCodePresenter
    {
        private readonly ISearchByAirlineCodeView _view;
        private readonly IFlightService _service;

        public SearchByAirlineCodePresenter(ISearchByAirlineCodeView view, IFlightService service)
        {
            _view = view;
            _service = service;
        }

        public void OnAirlineCodeChanged(IFormView source, ChangeEventArgs<string> args)
        {
            bool isValid = FlightValidator.IsAirlineCodeValid(args.Value);
            if (!isValid)
            {
                _view.SetFieldError(
                    nameof(_view.AirlineCode),
                    "Airline code must be 2-3 uppercased-alphanumeric characters."
                );
            }
        }

        public void OnSubmitted(IFormView source, SubmitEventArgs<string> args)
        {
            _view.Display(_service.FindAllHaving(args.Data));
            _view.Reset();
        }
    }
}
