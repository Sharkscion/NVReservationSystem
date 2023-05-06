using FlightReservation.Common.Validators;
using FlightReservation.Services.Contracts;
using FlightReservation.UI.Common;
using FlightReservation.UI.Presenters.FlightMaintenance.Contracts;
using FlightReservation.UI.Views.Contracts;
using FlightReservation.UI.Views.FlightMaintenance.Contracts;

namespace FlightReservation.UI.Presenters.FlightMaintenance
{
    internal class SearchByFlightNumberPresenter : ISearchByFlightNumberPresenter
    {
        private readonly ISearchByFlightNumberView _view;
        private readonly IFlightService _service;

        public SearchByFlightNumberPresenter(ISearchByFlightNumberView view, IFlightService service)
        {
            _view = view;
            _service = service;
        }

        public void OnFlightNumberChanged(IFormView source, ChangeEventArgs<int> args)
        {
            bool isValid = FlightValidator.IsFlightNumberValid(args.Value);
            if (!isValid)
            {
                _view.SetFlightNumberError("Flight number must be an integer between 1 and 9999.");
            }
        }

        public void OnSubmitted(IFormView source, SubmitEventArgs<int> args)
        {
            _view.Display(_service.FindAllHaving(args.Data));
            _view.Reset();
        }
    }
}
