using FlightPlaneris.Storage;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlaneris.Controllers
{
    [Route("testing-api")]
    [ApiController]
    public class CleanUpAPIController : ControllerBase
    {
        private readonly FlightStorage _storage;

        public CleanUpAPIController()
        {
            _storage = new FlightStorage();
        }

        [Route("clear")]
        [HttpPost]
        public IActionResult Clear()
        {
            return Ok();
        }
    }
}
