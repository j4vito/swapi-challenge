using Microsoft.EntityFrameworkCore;
using StarWars.Api.Data;
using StarWars.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient<SwapiService>();
builder.Services.AddScoped<ISwapiService>(provider => 
    new CachedSwapiService(
        provider.GetRequiredService<SwapiService>(), 
        provider.GetRequiredService<StarWarsDbContext>()));

builder.Services.AddDbContext<StarWarsDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection(); // Disable for simpler Docker setup if needed, or keep. Keeping it is fine usually.

app.UseAuthorization();

app.UseMiddleware<StarWars.Api.Middleware.RequestHistoryMiddleware>();

app.MapControllers();

// Apply migrations at startup
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<StarWarsDbContext>();
    db.Database.Migrate();
}

app.Run();
