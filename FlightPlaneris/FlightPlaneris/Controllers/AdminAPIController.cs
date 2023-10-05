using FlightPlaneris.Module;
using FlightPlaneris.Storage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace FlightPlaneris.Controllers
{
    [Authorize]
    [Route("admin-api")]
    [ApiController]
    public class AdminAPIController : ControllerBase
    {
        private readonly FlightStorage _storage;

        public AdminAPIController()
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
            _storage.AddFlight(flight);

            return Created("",flight);
        }
    }
}
