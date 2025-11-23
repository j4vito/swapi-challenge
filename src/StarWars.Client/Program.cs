using StarWars.Client;
using StarWars.Client.DTOs;

var baseUrl = Environment.GetEnvironmentVariable("API_URL") ?? "http://localhost:8080";
var client = new StarWarsApiClient(baseUrl);

Console.WriteLine("Welcome to Star Wars API Client!");
Console.WriteLine($"Connecting to API at {baseUrl}...");

while (true)
{
    Console.WriteLine("\nSelect an option:");
    Console.WriteLine("1. List Characters");
    Console.WriteLine("2. Search Characters");
    Console.WriteLine("3. List Planets");
    Console.WriteLine("4. List Films");
    Console.WriteLine("5. List Species");
    Console.WriteLine("6. List Vehicles");
    Console.WriteLine("7. List Starships");
    Console.WriteLine("8. Add Favorite");
    Console.WriteLine("9. Remove Favorite");
    Console.WriteLine("10. View Favorites");
    Console.WriteLine("11. View Request History");
    Console.WriteLine("0. Exit");
    Console.Write("> ");

    var input = Console.ReadLine();

    try
    {
        switch (input)
        {
            case "1":
                await ListCharacters(client);
                break;
            case "2":
                await SearchCharacters(client);
                break;
            case "3":
                await ListPlanets(client);
                break;
            case "4":
                await ListFilms(client);
                break;
            case "5":
                await ListSpecies(client);
                break;
            case "6":
                await ListVehicles(client);
                break;
            case "7":
                await ListStarships(client);
                break;
            case "8":
                await AddFavorite(client);
                break;
            case "9":
                await RemoveFavorite(client);
                break;
            case "10":
                await ViewFavorites(client);
                break;
            case "11":
                await ViewHistory(client);
                break;
            case "0":
                return;
            default:
                Console.WriteLine("Invalid option.");
                break;
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
    }
}

static async Task ListCharacters(StarWarsApiClient client)
{
    Console.Write("Enter page number (default 1): ");
    var pageInput = Console.ReadLine();
    int page = int.TryParse(pageInput, out var p) ? p : 1;

    var characters = await client.GetCharactersAsync(page);
    DisplayItems(characters, c => c.Name, c => c.Url);
}

static async Task SearchCharacters(StarWarsApiClient client)
{
    Console.Write("Enter name to search: ");
    var name = Console.ReadLine();
    if (string.IsNullOrWhiteSpace(name)) return;

    var characters = await client.SearchCharactersAsync(name);
    DisplayItems(characters, c => c.Name, c => c.Url);
}

static async Task ListPlanets(StarWarsApiClient client)
{
    Console.Write("Enter page number (default 1): ");
    var pageInput = Console.ReadLine();
    int page = int.TryParse(pageInput, out var p) ? p : 1;
    var items = await client.GetPlanetsAsync(page);
    DisplayItems(items, i => i.Name, i => i.Url);
}

static async Task ListFilms(StarWarsApiClient client)
{
    Console.Write("Enter page number (default 1): ");
    var pageInput = Console.ReadLine();
    int page = int.TryParse(pageInput, out var p) ? p : 1;
    var items = await client.GetFilmsAsync(page);
    DisplayItems(items, i => i.Title, i => i.Url);
}

static async Task ListSpecies(StarWarsApiClient client)
{
    Console.Write("Enter page number (default 1): ");
    var pageInput = Console.ReadLine();
    int page = int.TryParse(pageInput, out var p) ? p : 1;
    var items = await client.GetSpeciesAsync(page);
    DisplayItems(items, i => i.Name, i => i.Url);
}

static async Task ListVehicles(StarWarsApiClient client)
{
    Console.Write("Enter page number (default 1): ");
    var pageInput = Console.ReadLine();
    int page = int.TryParse(pageInput, out var p) ? p : 1;
    var items = await client.GetVehiclesAsync(page);
    DisplayItems(items, i => i.Name, i => i.Url);
}

static async Task ListStarships(StarWarsApiClient client)
{
    Console.Write("Enter page number (default 1): ");
    var pageInput = Console.ReadLine();
    int page = int.TryParse(pageInput, out var p) ? p : 1;
    var items = await client.GetStarshipsAsync(page);
    DisplayItems(items, i => i.Name, i => i.Url);
}

static async Task AddFavorite(StarWarsApiClient client)
{
    Console.Write("Enter Character ID to add: ");
    if (int.TryParse(Console.ReadLine(), out int id))
    {
        await client.AddFavoriteAsync(id);
        Console.WriteLine("Favorite added successfully.");
    }
    else
    {
        Console.WriteLine("Invalid ID.");
    }
}

static async Task RemoveFavorite(StarWarsApiClient client)
{
    Console.Write("Enter Character ID to remove: ");
    if (int.TryParse(Console.ReadLine(), out int id))
    {
        await client.RemoveFavoriteAsync(id);
        Console.WriteLine("Favorite removed successfully.");
    }
    else
    {
        Console.WriteLine("Invalid ID.");
    }
}

static async Task ViewFavorites(StarWarsApiClient client)
{
    var favorites = await client.GetFavoritesAsync();
    Console.WriteLine("\n--- Favorites ---");
    foreach (var fav in favorites)
    {
        Console.WriteLine($"ID: {fav.SwapiId} - Name: {fav.Name}");
    }
}

static async Task ViewHistory(StarWarsApiClient client)
{
    var history = await client.GetHistoryAsync();
    Console.WriteLine("\n--- Request History ---");
    foreach (var entry in history)
    {
        Console.WriteLine($"[{entry.Timestamp:HH:mm:ss}] {entry.Method} {entry.Path} ({entry.StatusCode}) - {entry.DurationMs}ms");
    }
}

static void DisplayItems<T>(List<T> items, Func<T, string> nameSelector, Func<T, string> urlSelector)
{
    Console.WriteLine("\n--- Results ---");
    foreach (var item in items)
    {
        var id = ExtractId(urlSelector(item));
        Console.WriteLine($"ID: {id} - {nameSelector(item)}");
    }
}

static string ExtractId(string url)
{
    if (string.IsNullOrEmpty(url)) return "?";
    var parts = url.TrimEnd('/').Split('/');
    return parts.LastOrDefault() ?? "?";
}
