# WeddingApp

A RESTful API built with ASP.NET Core 10, Entity Framework Core, MySQL, and Swagger for API documentation.

## Features

- ASP.NET Core 10 web API
- Swagger/OpenAPI documentation in development mode
- MySQL database support via Docker Compose
- CORS enabled for local frontend testing
- Guest management endpoints
- Authentication endpoint with a dummy token response

## Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/10.0)
- Docker Desktop (for running MySQL locally)

## Getting Started

1. Start the MySQL container:

```bash
docker compose up -d mysql
```

2. Restore and run the API:

```bash
cd WeddingApp
dotnet restore
dotnet run
```

The API will start on `https://localhost:<port>`.

## API Documentation

Swagger UI is available when the app runs in Development mode at:

- `/swagger` for the interactive UI
- `/swagger/v1/swagger.json` for the OpenAPI JSON document

This makes it easy to explore and test the available endpoints directly in the browser.

## Available Endpoints

### Authentication

- `POST /api/auth/login`
  - Authenticates a user with a username and password
  - Returns a dummy JWT-style token for local testing

### Guests

- `GET /api/guests`
  - Retrieves all guests
- `POST /api/guests`
  - Creates a new guest

### Weather

- `GET /api/weatherforecast`
  - Returns a sample weather forecast payload

## Configuration

The application uses a MySQL connection string from `appsettings.json` or `appsettings.Development.json`.

By default, the app expects a connection string named `DefaultConnection`. The Docker Compose setup provides a local MySQL instance with:

- Host: `localhost`
- Port: `3306`
- Database: `wedding`
- User: `wedding_user`
- Password: `wedding_pass`

## Project Structure

```text
WeddingApp/
├── Controller/
│   ├── AuthController.cs
│   ├── GuestsController.cs
│   └── WeatherForecastController.cs
├── Data/
│   └── AppDbContext.cs
├── Models/
│   └── Guest.cs
├── Program.cs
├── appsettings.json
├── appsettings.Development.json
└── WeddingApp.csproj
```

## Build

```bash
dotnet build
```

## License

MIT
