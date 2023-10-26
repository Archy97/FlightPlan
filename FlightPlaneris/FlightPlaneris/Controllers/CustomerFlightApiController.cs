using AutoMapper;
using FlightPlanner.Core.Interfaces;
using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;
using FlightPlanner.Module;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlanner.Controllers;

[Route("api")]
[ApiController]
public class CustomerFlightApiController : ControllerBase
{
    private readonly IAirportService _airportService;
    private readonly IFlightService _flightService;
    private readonly IMapper _mapper;
    private readonly IEnumerable<IValidateSearch> _validator;

    public CustomerFlightApiController(IAirportService airportService, IMapper mapper, IFlightService flightService,
        IEnumerable<IValidateSearch> validator)
    {
        _airportService = airportService;
        _mapper = mapper;
        _flightService = flightService;
        _validator = validator;
    }

    [Route("airports")]
    [HttpGet]
    public IActionResult SearchAirports(string search)
    {
        var airport = _airportService.FindAirport(search);

        if (airport.Count == 0) return NotFound();

        var result = airport.Select(a => _mapper.Map<AirportRequest>(a)).ToList();

        return Ok(result);
    }

    [Route("flights/{id}")]
    [HttpGet]
    public IActionResult FindFlightById(int id)
    {
        var result = _flightService.GetFullFlightById(id);

        if (result == null) return NotFound();

        return Ok(_mapper.Map<FlightRequest>(result));
    }

    [Route("flights/search")]
    [HttpPost]
    public IActionResult SearchFlights(SearchFlightsRequest req)
    {
        if (!_validator.All(v => v.IsValid(req))) return BadRequest();

        var flight = _flightService.SearchFlights(req);

        var pageResult = new PageResult<Flights>
        {
            Page = 0,
            TotalItems = flight.Count,
            Items = flight
        };

        if (req.from == req.to) return BadRequest();

        return Ok(pageResult);
    }
}