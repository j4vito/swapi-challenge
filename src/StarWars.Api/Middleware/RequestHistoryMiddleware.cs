using System.Diagnostics;
using StarWars.Api.Data;
using StarWars.Api.Entities;

namespace StarWars.Api.Middleware;

public class RequestHistoryMiddleware
{
    private readonly RequestDelegate _next;

    public RequestHistoryMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, StarWarsDbContext dbContext)
    {
        var stopwatch = Stopwatch.StartNew();
        
        await _next(context);
        
        stopwatch.Stop();

        var historyEntry = new RequestHistory
        {
            Timestamp = DateTime.UtcNow,
            Method = context.Request.Method,
            Path = context.Request.Path + context.Request.QueryString,
            StatusCode = context.Response.StatusCode,
            DurationMs = stopwatch.ElapsedMilliseconds
        };

        dbContext.RequestHistory.Add(historyEntry);
        await dbContext.SaveChangesAsync();
    }
}
