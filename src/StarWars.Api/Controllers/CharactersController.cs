using Microsoft.AspNetCore.Mvc;
using StarWars.Api.DTOs;
using StarWars.Api.Services;

namespace StarWars.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class CharactersController : ControllerBase
{
    private readonly ISwapiService _swapiService;

    public CharactersController(ISwapiService swapiService)
    {
        _swapiService = swapiService;
    }

    [HttpGet]
    public async Task<ActionResult<SwapiListResponse<SwapiPersonDto>>> GetCharacters([FromQuery] int page = 1, [FromQuery] string? name = null)
    {
        if (!string.IsNullOrWhiteSpace(name))
        {
            return Ok(await _swapiService.SearchPeopleAsync(name));
        }
        return Ok(await _swapiService.GetPeopleAsync(page));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<SwapiPersonDto>> GetCharacter(int id)
    {
        var person = await _swapiService.GetPersonAsync(id);
        if (person == null)
        {
            return NotFound();
        }
        return Ok(person);
    }
}
