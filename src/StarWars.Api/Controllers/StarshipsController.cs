using Microsoft.AspNetCore.Mvc;
using StarWars.Api.DTOs;
using StarWars.Api.Services;

namespace StarWars.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class StarshipsController : ControllerBase
{
    private readonly ISwapiService _swapiService;

    public StarshipsController(ISwapiService swapiService)
    {
        _swapiService = swapiService;
    }

    [HttpGet]
    public async Task<ActionResult<SwapiListResponse<SwapiStarshipDto>>> GetStarships([FromQuery] int page = 1)
    {
        return Ok(await _swapiService.GetStarshipsAsync(page));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<SwapiStarshipDto>> GetStarship(int id)
    {
        var result = await _swapiService.GetStarshipAsync(id);
        if (result == null) return NotFound();
        return Ok(result);
    }
}
