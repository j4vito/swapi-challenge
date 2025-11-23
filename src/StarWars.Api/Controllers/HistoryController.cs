using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StarWars.Api.Data;
using StarWars.Api.Entities;

namespace StarWars.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class HistoryController : ControllerBase
{
    private readonly StarWarsDbContext _dbContext;

    public HistoryController(StarWarsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<RequestHistory>>> GetHistory([FromQuery] int limit = 50)
    {
        var history = await _dbContext.RequestHistory
            .OrderByDescending(h => h.Timestamp)
            .Take(limit)
            .ToListAsync();
            
        return Ok(history);
    }
}
