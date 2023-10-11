using FlightPlaneris.Module;
using FlightPlaneris.Storage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace FlightPlaneris.Controllers
{
    [Authorize]
    [Route("admin-api")]
    [ApiController]
    public class AdminApiController : ControllerBase
    {
        private readonly FlightStorage _storage;
        private static readonly object AddLock = new();
        private static readonly object DeleteLock = new();

        public AdminApiController()
        {
            _storage = new FlightStorage();
        }

        [Route("flights/{id}")]
        [HttpGet]
        public IActionResult GetFlights(int id)
        {
            return NotFound();
        }

        [Route("flights")]
        [HttpPut]
        public IActionResult PutFlights(Flights flight)
        {
            lock (AddLock)
            {
                try
                {
                    _storage.AddFlight(flight);
                    return Created("", flight);
                }
                catch (InvalidFlightException)
                {
                    return BadRequest();
                }
                catch (WrongAirportException)
                {
                    return BadRequest();
                }
                catch (WrongDateException)
                {
                    return BadRequest();
                }
                catch (DuplicateFlightException)
                {
                    return Conflict();
                }
            }
        }

        [Route("flights/{id}")]
        [HttpDelete]
        public IActionResult DeleteFlights(int id)
        {
            lock (DeleteLock)
            {
            

                _storage.DeleteById(id);

                return Ok();
            }
        }
    }
}
