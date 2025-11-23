using Microsoft.AspNetCore.Mvc;
using StarWars.Api.DTOs;
using StarWars.Api.Services;

namespace StarWars.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class PlanetsController : ControllerBase
{
    private readonly ISwapiService _swapiService;

    public PlanetsController(ISwapiService swapiService)
    {
        _swapiService = swapiService;
    }

    [HttpGet]
    public async Task<ActionResult<SwapiListResponse<SwapiPlanetDto>>> GetPlanets([FromQuery] int page = 1)
    {
        return Ok(await _swapiService.GetPlanetsAsync(page));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<SwapiPlanetDto>> GetPlanet(int id)
    {
        var result = await _swapiService.GetPlanetAsync(id);
        if (result == null) return NotFound();
        return Ok(result);
    }
}
