using FlightPlanner.Core.Services;
using FlightPlanner.Data;

namespace FlightPlanner.Services;

public class CleanUpService : DbService, ICleanUpService
{
    public CleanUpService(IFlightPlannerDbContext context) : base(context)
    {
    }

    public void CleanUpDatabase()
    {
        _context.Airports.RemoveRange(_context.Airports);
        _context.Flights.RemoveRange(_context.Flights);
        _context.SaveChanges();
    }
}