using System.Runtime.Serialization;

namespace FlightPlaneris.Storage
{
    public class InvalidFlightException : Exception
    {
        public InvalidFlightException() : base("null or empty")
        {
        }
    }
}