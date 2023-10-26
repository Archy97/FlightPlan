using AutoMapper;
using FlightPlanner.Core.Interfaces;
using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;
using FlightPlanner.Module;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlanner.Controllers
{
    [Authorize]
    [Route("admin-api")]
    [ApiController]
    public class AdminApiController : ControllerBase
    {
        private readonly IFlightService _flightService;
        private readonly IMapper _mapper;
        private readonly IEnumerable<IValidate> _validator;
        private static readonly object Lock = new();

        public AdminApiController(IFlightService flightService, IMapper mapper, IEnumerable<IValidate> validator)
        {
            _mapper = mapper;
            _validator = validator;
            _flightService = flightService;
        }

        [Route("flights/{id}")]
        [HttpGet]
        public IActionResult GetFlight(int id)
        {
            var flight = _flightService.GetFullFlightById(id);

            if (flight == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<FlightRequest>(flight));
        }

        [Route("flights")]
        [HttpPut]
        public IActionResult PutFlights(FlightRequest request)
        {
            var flight = _mapper.Map<Flights>(request);

            lock (Lock)
            {
                if (!_validator.All(v => v.IsValid(flight)))
                {
                    return BadRequest();
                }

                if (_flightService.Exists(flight))
                {
                    return Conflict();
                }

                _flightService.Create(flight);
            }

            request = _mapper.Map<FlightRequest>(flight);

                return Created("", request);
        }

        [Route("flights/{id}")]
        [HttpDelete]
        public IActionResult DeleteFlights(int id)
        {
            lock (Lock)
            {
                var flight = _flightService.GetFullFlightById(id);
                if (flight != null) _flightService.Delete(flight);

                return Ok();
            }
        }
    }
}
