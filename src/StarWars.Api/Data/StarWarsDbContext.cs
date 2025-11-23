using Microsoft.EntityFrameworkCore;
using StarWars.Api.Entities;

namespace StarWars.Api.Data;

public class StarWarsDbContext : DbContext
{
    public StarWarsDbContext(DbContextOptions<StarWarsDbContext> options) : base(options)
    {
    }

    public DbSet<FavoriteCharacter> FavoriteCharacters { get; set; }
    public DbSet<RequestHistory> RequestHistory { get; set; }
    public DbSet<CachedCharacter> CachedCharacters { get; set; }
    public DbSet<ApiCacheEntry> ApiCacheEntries { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<FavoriteCharacter>()
            .HasIndex(f => f.SwapiId)
            .IsUnique();
            
        modelBuilder.Entity<CachedCharacter>()
            .Property(c => c.SwapiId)
            .ValueGeneratedNever();
    }
}
