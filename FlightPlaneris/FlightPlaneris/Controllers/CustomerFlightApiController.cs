using FlightPlaneris.Module;
using FlightPlaneris.Storage;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FlightPlaneris.Controllers
{
    [Route("api")]
    [ApiController]
    public class CustomerFlightApiController : ControllerBase
    {
        private readonly FlightStorage _storage;

        public CustomerFlightApiController()
        {
            _storage = new FlightStorage();
        }

        [Route("airports")]
        [HttpGet]
        public IActionResult SearchAirports(string search)
        {
           var flight = _storage.SearchAirport(search);

           var result = new[] { flight.From };

           return Ok(result);
        }

        [Route("flights/{id}")]
        [HttpGet]
        public IActionResult FindFlightById(int id)
        {
           var result = _storage.FindFlightById(id);

           if (result == null)
           {
               return NotFound();
           }

           return Ok(result);
        }

        [Route("flights/search")]
        [HttpPost]
        public IActionResult SearchFlights( SearchFlightsRequest req)
        {
            if (string.IsNullOrEmpty(req.from) || string.IsNullOrEmpty(req.to) || req.departureDate == default)
            {
                return BadRequest();
            }

            var flight = _storage.SearchFlights(req);

            if (req.from == req.to)
            {
                return BadRequest();
            }

            var pageResult = new PageResult<Flights>

            {  Page = 0,
               TotalItems = flight.Count,
               Items = flight
            };

            return Ok(pageResult);
        }
    }
}
