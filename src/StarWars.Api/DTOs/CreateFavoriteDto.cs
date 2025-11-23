using System.ComponentModel.DataAnnotations;

namespace StarWars.Api.DTOs;

public class CreateFavoriteDto
{
    [Required]
    public int SwapiId { get; set; }
}
