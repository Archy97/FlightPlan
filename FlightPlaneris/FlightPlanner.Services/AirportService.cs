using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;
using FlightPlanner.Data;

namespace FlightPlanner.Services;

public class AirportService : EntityService<Airport>, IAirportService
{
    public AirportService(IFlightPlannerDbContext context) : base(context)
    {
    }

    public List<Airport> FindAirport(string search)
    {
        search = search.Trim().ToLower();

        var airports = _context.Airports.AsEnumerable()
            .Where(flight =>
                flight.City.ToLower().Contains(search) ||
                flight.Country.ToLower().Contains(search) ||
                flight.AirportCode.ToLower().Contains(search))
            .ToList();

        return airports;
    }
}