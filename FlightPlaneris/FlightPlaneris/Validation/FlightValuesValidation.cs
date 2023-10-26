using FlightPlanner.Core.Interfaces;
using FlightPlanner.Core.Models;

namespace FlightPlanner.Validation;

public class FlightValuesValidation : IValidate
{
    public bool IsValid(Flights flight)
    {
        return !string.IsNullOrEmpty(flight?.Carrier) &&
               !string.IsNullOrEmpty(flight?.DepartureTime) &&
               !string.IsNullOrEmpty(flight?.ArrivalTime) &&
               flight?.To != null &&
               flight?.From != null;
    }
}