using FlightPlaneris.Module;
using FlightPlaneris.Storage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;



namespace FlightPlaneris.Controllers
{
    [Authorize]
    [Route("admin-api")]
    [ApiController]
    public class AdminApiController : ControllerBase
    {
        private readonly FlightStorage _storage;
        private static readonly object Lock = new();

        public AdminApiController(FlightStorage storage)
        {
            _storage = storage;
        }

        [Route("flights/{id}")]
        [HttpGet]
        public IActionResult GetFlight(int id)
        {
            var flight = _storage.GetFlights(id);

            if (flight == null)
            {
                return NotFound();
            }

            return Ok(flight);
        }

        [Route("flights")]
        [HttpPut]
        public IActionResult PutFlights(Flights flight)
        {
            lock (Lock)
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
            lock (Lock)
            {
                _storage.DeleteById(id);

                return Ok();
            }
        }
    }
}
