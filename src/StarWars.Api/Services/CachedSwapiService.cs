using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using StarWars.Api.Data;
using StarWars.Api.DTOs;
using StarWars.Api.Entities;

namespace StarWars.Api.Services;

public class CachedSwapiService : ISwapiService
{
    private readonly ISwapiService _innerService;
    private readonly StarWarsDbContext _dbContext;
    private readonly TimeSpan _cacheDuration = TimeSpan.FromMinutes(10);

    public CachedSwapiService(ISwapiService innerService, StarWarsDbContext dbContext)
    {
        _innerService = innerService;
        _dbContext = dbContext;
    }

    public async Task<SwapiListResponse<SwapiPersonDto>> GetPeopleAsync(int page = 1)
    {
        string key = $"people?page={page}";
        var cached = await _dbContext.ApiCacheEntries.FindAsync(key);

        if (cached != null && cached.Expiration > DateTime.UtcNow)
        {
            return JsonSerializer.Deserialize<SwapiListResponse<SwapiPersonDto>>(cached.JsonData) 
                   ?? new SwapiListResponse<SwapiPersonDto>();
        }

        var result = await _innerService.GetPeopleAsync(page);
        
        await UpdateCacheAsync(key, JsonSerializer.Serialize(result));
        
        return result;
    }

    public async Task<SwapiListResponse<SwapiPersonDto>> SearchPeopleAsync(string name)
    {
        string key = $"people?search={name}";
        var cached = await _dbContext.ApiCacheEntries.FindAsync(key);

        if (cached != null && cached.Expiration > DateTime.UtcNow)
        {
            return JsonSerializer.Deserialize<SwapiListResponse<SwapiPersonDto>>(cached.JsonData) 
                   ?? new SwapiListResponse<SwapiPersonDto>();
        }

        var result = await _innerService.SearchPeopleAsync(name);
        
        await UpdateCacheAsync(key, JsonSerializer.Serialize(result));
        
        return result;
    }

    public async Task<SwapiPersonDto?> GetPersonAsync(int id)
    {
        var cached = await _dbContext.CachedCharacters.FindAsync(id);

        if (cached != null && cached.LastUpdated > DateTime.UtcNow.Subtract(_cacheDuration))
        {
            return JsonSerializer.Deserialize<SwapiPersonDto>(cached.JsonData);
        }

        var result = await _innerService.GetPersonAsync(id);
        if (result == null) return null;

        if (cached == null)
        {
            cached = new CachedCharacter
            {
                SwapiId = id,
                Name = result.Name,
                JsonData = JsonSerializer.Serialize(result),
                LastUpdated = DateTime.UtcNow
            };
            _dbContext.CachedCharacters.Add(cached);
        }
        else
        {
            cached.Name = result.Name;
            cached.JsonData = JsonSerializer.Serialize(result);
            cached.LastUpdated = DateTime.UtcNow;
        }
        
        await _dbContext.SaveChangesAsync();
        
        return result;
    }

    private async Task UpdateCacheAsync(string key, string data)
    {
        var cached = await _dbContext.ApiCacheEntries.FindAsync(key);
        if (cached == null)
        {
            cached = new ApiCacheEntry
            {
                Key = key,
                JsonData = data,
                LastUpdated = DateTime.UtcNow,
                Expiration = DateTime.UtcNow.Add(_cacheDuration)
            };
            _dbContext.ApiCacheEntries.Add(cached);
        }
        else
        {
            cached.JsonData = data;
            cached.LastUpdated = DateTime.UtcNow;
            cached.Expiration = DateTime.UtcNow.Add(_cacheDuration);
        }
        await _dbContext.SaveChangesAsync();
    }

    // Generic helper for list caching
    private async Task<T> GetCachedListAsync<T>(string key, Func<Task<T>> fetchFn) where T : new()
    {
        var cached = await _dbContext.ApiCacheEntries.FindAsync(key);
        if (cached != null && cached.Expiration > DateTime.UtcNow)
        {
            return JsonSerializer.Deserialize<T>(cached.JsonData) ?? new T();
        }
        var result = await fetchFn();
        await UpdateCacheAsync(key, JsonSerializer.Serialize(result));
        return result;
    }

    // Generic helper for item caching (using ApiCacheEntry for simplicity on non-people items)
    private async Task<T?> GetCachedItemAsync<T>(string key, Func<Task<T?>> fetchFn)
    {
        var cached = await _dbContext.ApiCacheEntries.FindAsync(key);
        if (cached != null && cached.Expiration > DateTime.UtcNow)
        {
            return JsonSerializer.Deserialize<T>(cached.JsonData);
        }
        var result = await fetchFn();
        if (result != null)
        {
            await UpdateCacheAsync(key, JsonSerializer.Serialize(result));
        }
        return result;
    }

    public Task<SwapiListResponse<SwapiPlanetDto>> GetPlanetsAsync(int page = 1) =>
        GetCachedListAsync($"planets?page={page}", () => _innerService.GetPlanetsAsync(page));

    public Task<SwapiPlanetDto?> GetPlanetAsync(int id) =>
        GetCachedItemAsync($"planets/{id}", () => _innerService.GetPlanetAsync(id));

    public Task<SwapiListResponse<SwapiFilmDto>> GetFilmsAsync(int page = 1) =>
        GetCachedListAsync($"films?page={page}", () => _innerService.GetFilmsAsync(page));

    public Task<SwapiFilmDto?> GetFilmAsync(int id) =>
        GetCachedItemAsync($"films/{id}", () => _innerService.GetFilmAsync(id));

    public Task<SwapiListResponse<SwapiSpeciesDto>> GetSpeciesAsync(int page = 1) =>
        GetCachedListAsync($"species?page={page}", () => _innerService.GetSpeciesAsync(page));

    public Task<SwapiSpeciesDto?> GetSpeciesDetailAsync(int id) =>
        GetCachedItemAsync($"species/{id}", () => _innerService.GetSpeciesDetailAsync(id));

    public Task<SwapiListResponse<SwapiVehicleDto>> GetVehiclesAsync(int page = 1) =>
        GetCachedListAsync($"vehicles?page={page}", () => _innerService.GetVehiclesAsync(page));

    public Task<SwapiVehicleDto?> GetVehicleAsync(int id) =>
        GetCachedItemAsync($"vehicles/{id}", () => _innerService.GetVehicleAsync(id));

    public Task<SwapiListResponse<SwapiStarshipDto>> GetStarshipsAsync(int page = 1) =>
        GetCachedListAsync($"starships?page={page}", () => _innerService.GetStarshipsAsync(page));

    public Task<SwapiStarshipDto?> GetStarshipAsync(int id) =>
        GetCachedItemAsync($"starships/{id}", () => _innerService.GetStarshipAsync(id));
}
