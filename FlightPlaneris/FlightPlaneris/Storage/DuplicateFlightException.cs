using System.Runtime.Serialization;

namespace FlightPlaneris.Storage
{
    public class DuplicateFlightException : Exception
    {
        public DuplicateFlightException() : base("Duplicate Flight")
        {
        }
    }
}