using System.Text.Json.Serialization;

namespace StarWars.Api.DTOs;

public class SwapiListResponse<T>
{
    [JsonPropertyName("count")]
    public int Count { get; set; }
    
    [JsonPropertyName("next")]
    public string? Next { get; set; }
    
    [JsonPropertyName("previous")]
    public string? Previous { get; set; }
    
    [JsonPropertyName("results")]
    public List<T> Results { get; set; } = new();
}

public class SwapiPersonDto
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;
    
    [JsonPropertyName("height")]
    public string Height { get; set; } = string.Empty;
    
    [JsonPropertyName("mass")]
    public string Mass { get; set; } = string.Empty;
    
    [JsonPropertyName("hair_color")]
    public string HairColor { get; set; } = string.Empty;
    
    [JsonPropertyName("skin_color")]
    public string SkinColor { get; set; } = string.Empty;
    
    [JsonPropertyName("eye_color")]
    public string EyeColor { get; set; } = string.Empty;
    
    [JsonPropertyName("birth_year")]
    public string BirthYear { get; set; } = string.Empty;
    
    [JsonPropertyName("gender")]
    public string Gender { get; set; } = string.Empty;
    
    [JsonPropertyName("url")]
    public string Url { get; set; } = string.Empty;
}

public class SwapiPlanetDto
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;
    [JsonPropertyName("climate")]
    public string Climate { get; set; } = string.Empty;
    [JsonPropertyName("terrain")]
    public string Terrain { get; set; } = string.Empty;
    [JsonPropertyName("population")]
    public string Population { get; set; } = string.Empty;
    [JsonPropertyName("url")]
    public string Url { get; set; } = string.Empty;
}

public class SwapiFilmDto
{
    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;
    [JsonPropertyName("episode_id")]
    public int EpisodeId { get; set; }
    [JsonPropertyName("director")]
    public string Director { get; set; } = string.Empty;
    [JsonPropertyName("release_date")]
    public string ReleaseDate { get; set; } = string.Empty;
    [JsonPropertyName("url")]
    public string Url { get; set; } = string.Empty;
}

public class SwapiSpeciesDto
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;
    [JsonPropertyName("classification")]
    public string Classification { get; set; } = string.Empty;
    [JsonPropertyName("designation")]
    public string Designation { get; set; } = string.Empty;
    [JsonPropertyName("average_height")]
    public string AverageHeight { get; set; } = string.Empty;
    [JsonPropertyName("url")]
    public string Url { get; set; } = string.Empty;
}

public class SwapiVehicleDto
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;
    [JsonPropertyName("model")]
    public string Model { get; set; } = string.Empty;
    [JsonPropertyName("manufacturer")]
    public string Manufacturer { get; set; } = string.Empty;
    [JsonPropertyName("cost_in_credits")]
    public string CostInCredits { get; set; } = string.Empty;
    [JsonPropertyName("url")]
    public string Url { get; set; } = string.Empty;
}

public class SwapiStarshipDto
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;
    [JsonPropertyName("model")]
    public string Model { get; set; } = string.Empty;
    [JsonPropertyName("manufacturer")]
    public string Manufacturer { get; set; } = string.Empty;
    [JsonPropertyName("starship_class")]
    public string StarshipClass { get; set; } = string.Empty;
    [JsonPropertyName("url")]
    public string Url { get; set; } = string.Empty;
}
