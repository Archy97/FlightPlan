using FlightPlanner.Core.Interfaces;
using FlightPlanner.Core.Models;

namespace FlightPlanner.Validation;

public class SameAirportValidation : IValidate
{
    public bool IsValid(Flights flight)
    {
        return flight?.To?.AirportCode?.Trim()?.ToLower() != flight?.From?.AirportCode?.Trim()?.ToLower();
    }
}