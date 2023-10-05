using FlightPlaneris.Module;

namespace FlightPlaneris.Storage
{
    public class FlightStorage
    {
        private static List<Flights> _flightStorage = new List<Flights>();
        private static int _id = 0;

        public void AddFlight(Flights flight)
        {
            flight.Id = _id++; 
            _flightStorage.Add(flight);
            
        }

        public void Clear()
        {
            _flightStorage.Clear();
        }
    }
}
