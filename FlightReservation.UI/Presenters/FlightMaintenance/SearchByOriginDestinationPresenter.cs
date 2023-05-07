using FlightReservation.Common.Validators;
using FlightReservation.Services.Contracts;
using FlightReservation.UI.Common;
using FlightReservation.UI.Presenters.FlightMaintenance.Contracts;
using FlightReservation.UI.Views.Contracts;
using FlightReservation.UI.Views.FlightMaintenance.Contracts;

namespace FlightReservation.UI.Presenters.FlightMaintenance
{
    internal class SearchByOriginDestinationPresenter : ISearchByOriginDestinationPresenter
    {
        private readonly ISearchByOriginDestinationView _view;
        private readonly IFlightService _service;

        public SearchByOriginDestinationPresenter(
            ISearchByOriginDestinationView view,
            IFlightService service
        )
        {
            _view = view;
            _service = service;
        }

        public void OnArrivalStationChanged(IFormView source, ChangeEventArgs<string> args)
        {
            bool isValid = FlightValidator.IsStationFormatValid(args.Value);
            if (!isValid)
            {
                _view.SetFieldError(
                    nameof(_view.ArrivalStation),
                    "Arrival station code must be 3 uppercased-alphanumeric characters."
                );
                return;
            }

            if (args.Value == _view.DepartureStation)
            {
                _view.SetFieldError(
                    nameof(_view.ArrivalStation),
                    "Arrival station must not be the same as the departure station."
                );
            }
        }

        public void OnDepartureStationChanged(IFormView source, ChangeEventArgs<string> args)
        {
            bool isValid = FlightValidator.IsStationFormatValid(args.Value);
            if (!isValid)
            {
                _view.SetFieldError(
                    nameof(_view.DepartureStation),
                    "Departure station code must be 3 uppercased-alphanumeric characters."
                );
                return;
            }

            if (args.Value == _view.ArrivalStation)
            {
                _view.SetFieldError(
                    nameof(_view.DepartureStation),
                    "Departure station must not be the same as the arrival station."
                );
            }
        }

        public void OnSubmitted(IFormView source, SubmitEventArgs<Tuple<string, string>> args)
        {
            _view.Display(
                _service.FindAllHaving(origin: args.Data.Item1, destination: args.Data.Item2)
            );
            _view.Reset();
        }
    }
}
