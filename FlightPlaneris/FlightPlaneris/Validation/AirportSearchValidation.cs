using FlightPlanner.Core.Interfaces;
using FlightPlanner.Core.Models;

namespace FlightPlanner.Validation;

public class AirportSearchValidation : IValidateSearch
{
    public bool IsValid(SearchFlightsRequest searchFlightRequest)
    {
        return !string.IsNullOrEmpty(searchFlightRequest.departureDate) &&
               !string.IsNullOrEmpty(searchFlightRequest.from) &&
               !string.IsNullOrEmpty(searchFlightRequest.to);
    }
}