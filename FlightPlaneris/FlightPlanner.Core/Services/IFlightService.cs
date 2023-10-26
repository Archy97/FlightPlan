using FlightPlanner.Core.Models;

namespace FlightPlanner.Core.Services;

public interface IFlightService : IEntityService<Flights>
{
    public Flights? GetFullFlightById(int id);
    public bool Exists(Flights flight);
    public List<Flights> SearchFlights(SearchFlightsRequest req);
}