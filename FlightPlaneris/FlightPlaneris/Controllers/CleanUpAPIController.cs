using FlightPlaneris.Storage;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlaneris.Controllers
{
    [Route("testing-api")]
    [ApiController]
    public class CleanUpApiController : ControllerBase
    {
        private readonly FlightStorage _storage;
     
        /// <inheritdoc />
        public CleanUpApiController(FlightStorage storage)
        {
            _storage = storage;

        }

        [Route("clear")]
        [HttpPost]

        public IActionResult Clear()
        {
            _storage.Clear();

            return Ok();
        }
    }
}