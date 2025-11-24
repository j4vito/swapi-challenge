# Star Wars API Microservice

A microservice that integrates with the Star Wars API (SWAPI), providing caching, favorite management, and request history logging. Includes a console client for interaction.

## Features

- **SWAPI Integration**: Fetches people and searches by name.
- **Caching**: Caches SWAPI responses in PostgreSQL to reduce external calls.
- **Favorites**: Manage favorite characters.
- **Request History**: Logs all API requests with duration and status.
- **Dockerized**: Complete setup with Docker Compose.

## Tech Stack

- **Language**: C# (.NET 8)
- **Database**: PostgreSQL
- **ORM**: Entity Framework Core
- **Containerization**: Docker

## Prerequisites

- Docker & Docker Compose

## How to Run

1. **Clone the repository**
   ```bash
   git clone https://github.com/j4vito/swapi-challenge.git
   cd StarWars
   ```

2. **Start the services**
   ```bash
   docker-compose up --build
   ```
   This will start:
   - PostgreSQL database (port 5432)
   - StarWars API (port 8080)

   The API will automatically apply database migrations on startup.

3. **Access the API Documentation**
   - Swagger UI: [http://localhost:8080/swagger](http://localhost:8080/swagger)

4. **Run the Console Client**
   You can run the client from your local machine (requires .NET 8 SDK) or use a container.

   **Local:**
   ```bash
   dotnet run --project src/StarWars.Client/StarWars.Client.csproj
   ```

   **Docker (One-off):**
   ```bash
   docker run --rm -it --network host mcr.microsoft.com/dotnet/sdk:8.0 dotnet run --project /app/src/StarWars.Client/StarWars.Client.csproj
   # Note: You need to mount the source code to /app or build a client image.
   ```
   
   *Easier way if you have .NET installed locally is just `dotnet run`.*

## API Endpoints

### Characters
- `GET /api/v1/characters?page={page}`: List characters (cached).
- `GET /api/v1/characters?name={name}`: Search characters (cached).
- `GET /api/v1/characters/{id}`: Get character details (cached).

### Favorites
- `GET /api/v1/favorites`: List favorites.
- `POST /api/v1/favorites`: Add favorite (Body: `{ "swapiId": 1 }`).
- `DELETE /api/v1/favorites/{id}`: Remove favorite.

### History
- `GET /api/v1/history`: View request history.

## Testing

Run unit tests:
```bash
dotnet test
```
