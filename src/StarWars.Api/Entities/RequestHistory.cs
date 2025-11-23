using System.ComponentModel.DataAnnotations;

namespace StarWars.Api.Entities;

public class RequestHistory
{
    public int Id { get; set; }
    
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    
    [Required]
    [MaxLength(10)]
    public string Method { get; set; } = string.Empty;
    
    [Required]
    [MaxLength(2048)]
    public string Path { get; set; } = string.Empty;
    
    public int StatusCode { get; set; }
    
    public long DurationMs { get; set; }
}
