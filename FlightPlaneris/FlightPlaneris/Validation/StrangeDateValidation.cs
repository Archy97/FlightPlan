﻿using FlightPlanner.Core.Interfaces;
using FlightPlanner.Core.Models;

namespace FlightPlanner.Validation;

public class StrangeDateValidation : IValidate
{
    public bool IsValid(Flights flight)
    {
        if (DateTime.TryParse(flight.ArrivalTime, out var arrivalTime) &&
            DateTime.TryParse(flight.DepartureTime, out var departureTime))
            return arrivalTime > departureTime;

        return false;
    }
}