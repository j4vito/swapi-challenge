using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StarWars.Api.Data;
using StarWars.Api.DTOs;
using StarWars.Api.Entities;
using StarWars.Api.Services;

namespace StarWars.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class FavoritesController : ControllerBase
{
    private readonly StarWarsDbContext _dbContext;
    private readonly ISwapiService _swapiService;

    public FavoritesController(StarWarsDbContext dbContext, ISwapiService swapiService)
    {
        _dbContext = dbContext;
        _swapiService = swapiService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<FavoriteCharacter>>> GetFavorites()
    {
        return Ok(await _dbContext.FavoriteCharacters.ToListAsync());
    }

    [HttpPost]
    public async Task<ActionResult<FavoriteCharacter>> AddFavorite(CreateFavoriteDto dto)
    {
        if (await _dbContext.FavoriteCharacters.AnyAsync(f => f.SwapiId == dto.SwapiId))
        {
            return Conflict(new { message = "Character already in favorites" });
        }

        // Fetch character details to get the name
        var person = await _swapiService.GetPersonAsync(dto.SwapiId);
        if (person == null)
        {
            return NotFound(new { message = "Character not found in SWAPI" });
        }

        var favorite = new FavoriteCharacter
        {
            SwapiId = dto.SwapiId,
            Name = person.Name
        };

        _dbContext.FavoriteCharacters.Add(favorite);
        await _dbContext.SaveChangesAsync();

        return CreatedAtAction(nameof(GetFavorites), new { id = favorite.Id }, favorite);
    }

    [HttpDelete("{swapiId}")]
    public async Task<IActionResult> RemoveFavorite(int swapiId)
    {
        var favorite = await _dbContext.FavoriteCharacters.FirstOrDefaultAsync(f => f.SwapiId == swapiId);
        if (favorite == null)
        {
            return NotFound();
        }

        _dbContext.FavoriteCharacters.Remove(favorite);
        await _dbContext.SaveChangesAsync();

        return NoContent();
    }
}
