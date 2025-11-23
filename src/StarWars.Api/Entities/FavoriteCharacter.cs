using System.ComponentModel.DataAnnotations;

namespace StarWars.Api.Entities;

public class FavoriteCharacter
{
    public int Id { get; set; }
    
    [Required]
    public int SwapiId { get; set; }
    
    [Required]
    [MaxLength(200)]
    public string Name { get; set; } = string.Empty;
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
