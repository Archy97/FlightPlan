using FlightPlaneris.Module;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FlightPlaneris.Storage
{
    public class FlightStorage
    {
        private static List<Flights> _flightStorage = new List<Flights>();
        private static int _id;
        private readonly FlightPlannerDbContext _context;

        public FlightStorage(FlightPlannerDbContext context)
        {
            _context = context;
        }

        public Flights GetFlight(int id)
        {
            return   _context.Flights.
                Include(f => f.From).
                Include(f => f.To).FirstOrDefault(f => f.Id == id);
        }

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

            var duplicate = _context.Flights.Where(f =>
                f.From.AirportCode.Equals(flight.From.AirportCode) &&
                f.DepartureTime.Equals(flight.DepartureTime) &&
                f.To.AirportCode.Equals(flight.To.AirportCode) &&
                f.ArrivalTime.Equals(flight.ArrivalTime)
            ).ToList();

            if (duplicate.Count > 0)
            {
                throw new DuplicateFlightException();
            }

            _context.Flights.Add(flight);
            _context.SaveChanges();
        }

        public void DeleteById(int id)
        {

            var flightToDelete = _context.Flights.FirstOrDefault(f => f.Id == id);

            if (flightToDelete != null)
            {
                _context.Flights.Remove(flightToDelete);
                _context.SaveChanges();
               
            }

            
        }

        public void Clear()
        {
            _context.Flights.RemoveRange(_context.Flights);
            _context.Airports.RemoveRange(_context.Airports);
            _context.SaveChanges();
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
            return _context.Flights.FirstOrDefault(f => f.Id == id);
        }

        public List<Flights> SearchFlights(SearchFlightsRequest req)
        {
            return _flightStorage.Where(r => r.From.AirportCode.Equals(req.from)
                                             && r.To.AirportCode.Equals(req.to)
                                             && r.DepartureTime.Contains(req.departureDate)).ToList();
        }
    }
}
