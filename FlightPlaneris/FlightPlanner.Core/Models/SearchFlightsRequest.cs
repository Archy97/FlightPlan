﻿namespace FlightPlanner.Core.Models
{
    public class SearchFlightsRequest
    {
        public string from { get; set; }
        public string to { get; set; }
        public string departureDate {get; set; }
    }
}
