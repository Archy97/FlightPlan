using System.Runtime.Serialization;

namespace FlightPlaneris.Storage
{
    public class WrongAirportException : Exception
    {
        public WrongAirportException() : base ("Wrong Airport")
        {
        }
    }
}