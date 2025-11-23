using System.ComponentModel.DataAnnotations;

namespace StarWars.Api.Entities;

public class ApiCacheEntry
{
    [Key]
    [MaxLength(255)]
    public string Key { get; set; } = string.Empty;
    
    [Required]
    public string JsonData { get; set; } = string.Empty;
    
    public DateTime LastUpdated { get; set; } = DateTime.UtcNow;
    
    public DateTime Expiration { get; set; }
}
