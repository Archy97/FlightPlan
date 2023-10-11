using System.Runtime.Serialization;

namespace FlightPlaneris.Storage
{
    public class WrongDateException : Exception
    {
        public WrongDateException() : base("Wrong Date")
        {
        }
    }
}