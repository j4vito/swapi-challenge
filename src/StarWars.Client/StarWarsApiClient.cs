using System.Net.Http.Json;
using System.Text.Json;
using StarWars.Client.DTOs;

namespace StarWars.Client;

public class StarWarsApiClient
{
    private readonly HttpClient _httpClient;

    public StarWarsApiClient(string baseUrl)
    {
        _httpClient = new HttpClient { BaseAddress = new Uri(baseUrl) };
    }

    public async Task<List<PersonDto>> GetCharactersAsync(int page = 1)
    {
        var response = await _httpClient.GetFromJsonAsync<ListResponse<PersonDto>>($"api/v1/characters?page={page}");
        return response?.Results ?? new List<PersonDto>();
    }

    public async Task<List<PersonDto>> SearchCharactersAsync(string name)
    {
        var response = await _httpClient.GetFromJsonAsync<ListResponse<PersonDto>>($"api/v1/characters?name={name}");
        return response?.Results ?? new List<PersonDto>();
    }

    public async Task<List<PlanetDto>> GetPlanetsAsync(int page = 1)
    {
        var response = await _httpClient.GetFromJsonAsync<ListResponse<PlanetDto>>($"api/v1/planets?page={page}");
        return response?.Results ?? new List<PlanetDto>();
    }

    public async Task<List<FilmDto>> GetFilmsAsync(int page = 1)
    {
        var response = await _httpClient.GetFromJsonAsync<ListResponse<FilmDto>>($"api/v1/films?page={page}");
        return response?.Results ?? new List<FilmDto>();
    }

    public async Task<List<SpeciesDto>> GetSpeciesAsync(int page = 1)
    {
        var response = await _httpClient.GetFromJsonAsync<ListResponse<SpeciesDto>>($"api/v1/species?page={page}");
        return response?.Results ?? new List<SpeciesDto>();
    }

    public async Task<List<VehicleDto>> GetVehiclesAsync(int page = 1)
    {
        var response = await _httpClient.GetFromJsonAsync<ListResponse<VehicleDto>>($"api/v1/vehicles?page={page}");
        return response?.Results ?? new List<VehicleDto>();
    }

    public async Task<List<StarshipDto>> GetStarshipsAsync(int page = 1)
    {
        var response = await _httpClient.GetFromJsonAsync<ListResponse<StarshipDto>>($"api/v1/starships?page={page}");
        return response?.Results ?? new List<StarshipDto>();
    }

    public async Task AddFavoriteAsync(int swapiId)
    {
        var response = await _httpClient.PostAsJsonAsync("api/v1/favorites", new { swapiId });
        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            throw new Exception($"Failed to add favorite: {response.StatusCode} - {error}");
        }
    }

    public async Task RemoveFavoriteAsync(int swapiId)
    {
        var response = await _httpClient.DeleteAsync($"api/v1/favorites/{swapiId}");
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Failed to remove favorite: {response.StatusCode}");
        }
    }

    public async Task<List<FavoriteDto>> GetFavoritesAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<FavoriteDto>>("api/v1/favorites") ?? new List<FavoriteDto>();
    }

    public async Task<List<HistoryDto>> GetHistoryAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<HistoryDto>>("api/v1/history") ?? new List<HistoryDto>();
    }
}
