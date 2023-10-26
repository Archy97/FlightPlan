using FlightPlanner.Core.Models;

namespace FlightPlanner.Core.Interfaces
{
    public interface IValidateSearch
    {
        bool IsValid(SearchFlightsRequest searchFlightRequest);
    }
}
