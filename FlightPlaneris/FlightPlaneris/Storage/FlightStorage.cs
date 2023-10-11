using FlightPlaneris.Module;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlaneris.Storage
{
    public class FlightStorage
    {
        private static List<Flights> _flightStorage = new List<Flights>();
        private static int _id;
       

        public void AddFlight(Flights flight)
        {
            if (string.IsNullOrEmpty(flight.To.Country) ||
                string.IsNullOrEmpty(flight.To.City) ||
                string.IsNullOrEmpty(flight.To.AirportCode) ||
                string.IsNullOrEmpty(flight.From.Country) ||
                string.IsNullOrEmpty(flight.From.City) ||
                string.IsNullOrEmpty(flight.From.AirportCode) ||
                string.IsNullOrEmpty(flight.Carrier) ||
                string.IsNullOrEmpty(flight.DepartureTime) ||
                string.IsNullOrEmpty(flight.ArrivalTime))
            {
                throw new InvalidFlightException();
            }

            if (flight.From.AirportCode.ToLower().Trim().Equals(flight.To.AirportCode.ToLower().Trim()))
             
            {
                throw new WrongAirportException();
            }

            if (DateTime.Parse(flight.ArrivalTime).CompareTo(DateTime.Parse(flight.DepartureTime)) <= 0)
            {
                throw new WrongDateException();
            }

            var duplicate =_flightStorage.Where(f =>
                f.From.AirportCode.Equals(flight.From.AirportCode) &&
                f.DepartureTime.Equals(flight.DepartureTime) &&
                f.To.AirportCode.Equals(flight.To.AirportCode) &&
                f.ArrivalTime.Equals(flight.ArrivalTime)
            ).ToList();

            if (duplicate.Count > 0)
            {
                throw new DuplicateFlightException();
            }

            flight.Id = _id++;
            _flightStorage.Add(flight);
        }

        public bool DeleteById(int id)
        {
            Flights flightToDelete = _flightStorage.FirstOrDefault(f => f.Id == id);

            if (flightToDelete != null)
            {
                _flightStorage.Remove(flightToDelete);
                return true; 
            }

            return false; 
        }

        public void Clear()
        {
            _flightStorage.Clear();
        }

        public Flights SearchAirport(string search)
        {
            var flight = 
                _flightStorage.FirstOrDefault(s => new[] { s.From.Country , s.From.City , s.From.AirportCode}
                .Any(att => att.ToLower().Contains(search.ToLower().Trim())));

            return flight;
        }

        public Flights FindFlightById(int id)
        {
            return _flightStorage.FirstOrDefault(f => f.Id == id);
        }

        public List<Flights> SearchFlights(SearchFlightsRequest req)
        {
            return _flightStorage.Where(r => r.From.AirportCode.Equals(req.from)
                                             && r.To.AirportCode.Equals(req.to)
                                             && r.DepartureTime.Contains(req.departureDate)).ToList();
        }
    }
}
