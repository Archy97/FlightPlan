﻿using System.Text.Json.Serialization;

namespace FlightPlaneris.Module
{
    public class Airport
    {
        public string Country { get; set; }
        public string City { get; set; }

        [JsonPropertyName("airport")]
        public string AirportCode { get; set; }

      
    }
}
