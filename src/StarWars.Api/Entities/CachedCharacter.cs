using System.ComponentModel.DataAnnotations;

namespace StarWars.Api.Entities;

public class CachedCharacter
{
    [Key]
    public int SwapiId { get; set; }
    
    [Required]
    [MaxLength(200)]
    public string Name { get; set; } = string.Empty;
    
    [Required]
    public string JsonData { get; set; } = string.Empty;
    
    public DateTime LastUpdated { get; set; } = DateTime.UtcNow;
}
