using Microsoft.AspNetCore.Mvc;
using StarWars.Api.DTOs;
using StarWars.Api.Services;

namespace StarWars.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class VehiclesController : ControllerBase
{
    private readonly ISwapiService _swapiService;

    public VehiclesController(ISwapiService swapiService)
    {
        _swapiService = swapiService;
    }

    [HttpGet]
    public async Task<ActionResult<SwapiListResponse<SwapiVehicleDto>>> GetVehicles([FromQuery] int page = 1)
    {
        return Ok(await _swapiService.GetVehiclesAsync(page));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<SwapiVehicleDto>> GetVehicle(int id)
    {
        var result = await _swapiService.GetVehicleAsync(id);
        if (result == null) return NotFound();
        return Ok(result);
    }
}
