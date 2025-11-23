using System.Text.Json;
using StarWars.Api.DTOs;

namespace StarWars.Api.Services;

public class SwapiService : ISwapiService
{
    private readonly HttpClient _httpClient;
    private const string BaseUrl = "https://swapi.dev/api/";

    public SwapiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri(BaseUrl);
    }

    public async Task<SwapiListResponse<SwapiPersonDto>> GetPeopleAsync(int page = 1)
    {
        var response = await _httpClient.GetAsync($"people/?page={page}");
        response.EnsureSuccessStatusCode();
        
        var content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<SwapiListResponse<SwapiPersonDto>>(content) 
               ?? new SwapiListResponse<SwapiPersonDto>();
    }

    public async Task<SwapiListResponse<SwapiPersonDto>> SearchPeopleAsync(string name)
    {
        var response = await _httpClient.GetAsync($"people/?search={name}");
        response.EnsureSuccessStatusCode();
        
        var content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<SwapiListResponse<SwapiPersonDto>>(content) 
               ?? new SwapiListResponse<SwapiPersonDto>();
    }

    public async Task<SwapiPersonDto?> GetPersonAsync(int id)
    {
        var response = await _httpClient.GetAsync($"people/{id}/");
        if (!response.IsSuccessStatusCode) return null;
        
        var content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<SwapiPersonDto>(content);
    }

    public async Task<SwapiListResponse<SwapiPlanetDto>> GetPlanetsAsync(int page = 1)
    {
        var response = await _httpClient.GetAsync($"planets/?page={page}");
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<SwapiListResponse<SwapiPlanetDto>>(content) ?? new();
    }

    public async Task<SwapiPlanetDto?> GetPlanetAsync(int id)
    {
        var response = await _httpClient.GetAsync($"planets/{id}/");
        if (!response.IsSuccessStatusCode) return null;
        var content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<SwapiPlanetDto>(content);
    }

    public async Task<SwapiListResponse<SwapiFilmDto>> GetFilmsAsync(int page = 1)
    {
        var response = await _httpClient.GetAsync($"films/?page={page}");
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<SwapiListResponse<SwapiFilmDto>>(content) ?? new();
    }

    public async Task<SwapiFilmDto?> GetFilmAsync(int id)
    {
        var response = await _httpClient.GetAsync($"films/{id}/");
        if (!response.IsSuccessStatusCode) return null;
        var content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<SwapiFilmDto>(content);
    }

    public async Task<SwapiListResponse<SwapiSpeciesDto>> GetSpeciesAsync(int page = 1)
    {
        var response = await _httpClient.GetAsync($"species/?page={page}");
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<SwapiListResponse<SwapiSpeciesDto>>(content) ?? new();
    }

    public async Task<SwapiSpeciesDto?> GetSpeciesDetailAsync(int id)
    {
        var response = await _httpClient.GetAsync($"species/{id}/");
        if (!response.IsSuccessStatusCode) return null;
        var content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<SwapiSpeciesDto>(content);
    }

    public async Task<SwapiListResponse<SwapiVehicleDto>> GetVehiclesAsync(int page = 1)
    {
        var response = await _httpClient.GetAsync($"vehicles/?page={page}");
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<SwapiListResponse<SwapiVehicleDto>>(content) ?? new();
    }

    public async Task<SwapiVehicleDto?> GetVehicleAsync(int id)
    {
        var response = await _httpClient.GetAsync($"vehicles/{id}/");
        if (!response.IsSuccessStatusCode) return null;
        var content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<SwapiVehicleDto>(content);
    }

    public async Task<SwapiListResponse<SwapiStarshipDto>> GetStarshipsAsync(int page = 1)
    {
        var response = await _httpClient.GetAsync($"starships/?page={page}");
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<SwapiListResponse<SwapiStarshipDto>>(content) ?? new();
    }

    public async Task<SwapiStarshipDto?> GetStarshipAsync(int id)
    {
        var response = await _httpClient.GetAsync($"starships/{id}/");
        if (!response.IsSuccessStatusCode) return null;
        var content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<SwapiStarshipDto>(content);
    }
}
