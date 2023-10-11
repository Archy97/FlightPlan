using System.Runtime.Serialization;

namespace FlightPlaneris
{
    public class DuplicateFlightException : Exception
    {
        public DuplicateFlightException(): base("Duplicate Flight")
        {
        }
    }
}