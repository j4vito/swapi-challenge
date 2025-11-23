using Microsoft.AspNetCore.Mvc;
using StarWars.Api.DTOs;
using StarWars.Api.Services;

namespace StarWars.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class FilmsController : ControllerBase
{
    private readonly ISwapiService _swapiService;

    public FilmsController(ISwapiService swapiService)
    {
        _swapiService = swapiService;
    }

    [HttpGet]
    public async Task<ActionResult<SwapiListResponse<SwapiFilmDto>>> GetFilms([FromQuery] int page = 1)
    {
        return Ok(await _swapiService.GetFilmsAsync(page));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<SwapiFilmDto>> GetFilm(int id)
    {
        var result = await _swapiService.GetFilmAsync(id);
        if (result == null) return NotFound();
        return Ok(result);
    }
}
