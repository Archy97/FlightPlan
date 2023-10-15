using FlightPlaneris.Module;
using Microsoft.EntityFrameworkCore;

namespace FlightPlaneris
{
    public class FlightPlannerDbContext : DbContext
    {
        public FlightPlannerDbContext(DbContextOptions<FlightPlannerDbContext> options) : base(options)
        {
           
        }

        public DbSet<Flights> Flights { get; set; }

        public DbSet<Airport> Airports { get; set; }

    }
}
