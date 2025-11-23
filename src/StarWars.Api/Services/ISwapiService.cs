using StarWars.Api.DTOs;

namespace StarWars.Api.Services;

public interface ISwapiService
{
    Task<SwapiListResponse<SwapiPersonDto>> GetPeopleAsync(int page = 1);
    Task<SwapiListResponse<SwapiPersonDto>> SearchPeopleAsync(string name);
    Task<SwapiPersonDto?> GetPersonAsync(int id);

    Task<SwapiListResponse<SwapiPlanetDto>> GetPlanetsAsync(int page = 1);
    Task<SwapiPlanetDto?> GetPlanetAsync(int id);

    Task<SwapiListResponse<SwapiFilmDto>> GetFilmsAsync(int page = 1);
    Task<SwapiFilmDto?> GetFilmAsync(int id);

    Task<SwapiListResponse<SwapiSpeciesDto>> GetSpeciesAsync(int page = 1);
    Task<SwapiSpeciesDto?> GetSpeciesDetailAsync(int id);

    Task<SwapiListResponse<SwapiVehicleDto>> GetVehiclesAsync(int page = 1);
    Task<SwapiVehicleDto?> GetVehicleAsync(int id);

    Task<SwapiListResponse<SwapiStarshipDto>> GetStarshipsAsync(int page = 1);
    Task<SwapiStarshipDto?> GetStarshipAsync(int id);
}
