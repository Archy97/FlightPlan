using FlightPlanner.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlanner.Controllers;

[Route("testing-api")]
[ApiController]
public class CleanUpApiController : ControllerBase
{
    private readonly ICleanUpService _cleanUpService;

    public CleanUpApiController(ICleanUpService cleanUpService)
    {
        _cleanUpService = cleanUpService;
    }

    [Route("clear")]
    [HttpPost]
    public IActionResult Clear()
    {
        _cleanUpService.CleanUpDatabase();

        return Ok();
    }
}