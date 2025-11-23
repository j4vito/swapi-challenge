using Microsoft.AspNetCore.Mvc;
using StarWars.Api.DTOs;
using StarWars.Api.Services;

namespace StarWars.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class SpeciesController : ControllerBase
{
    private readonly ISwapiService _swapiService;

    public SpeciesController(ISwapiService swapiService)
    {
        _swapiService = swapiService;
    }

    [HttpGet]
    public async Task<ActionResult<SwapiListResponse<SwapiSpeciesDto>>> GetSpecies([FromQuery] int page = 1)
    {
        return Ok(await _swapiService.GetSpeciesAsync(page));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<SwapiSpeciesDto>> GetSpeciesDetail(int id)
    {
        var result = await _swapiService.GetSpeciesDetailAsync(id);
        if (result == null) return NotFound();
        return Ok(result);
    }
}
