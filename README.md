# Walks

A RESTful ASP.NET Core Web API for managing walking tracks, regions, images, and authentication for New Zealand walks.

## Features

- CRUD operations for Regions, Walks, and Images
- User authentication and JWT-based authorization
- File upload support for walk and region images
- Filtering, sorting, and pagination for walks
- Custom exception handling and logging
- Entity Framework Core with SQL Server
- AutoMapper for DTO mapping
- Role-based access control

## Project Structure

- `Controllers/` — API endpoints for Regions, Walks, Images, and Authentication
- `Repositories/` — Data access logic for each entity
- `Models/Domain/` — Entity models
- `Models/DTO/` — Data transfer objects
- `Data/` — Database context and migrations
- `Middlewares/` — Custom middleware (e.g., exception handler)
- `Mappings/` — AutoMapper profiles
- `Uploads/` — Uploaded image files (gitignored)

## Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)

### Setup

1. Clone the repository:
   ```bash
   git clone https://github.com/eliya-it/walks
   cd walks
   ```
2. Update the connection strings in `appsettings.json` and `appsettings.Development.json` for your SQL Server instance.
3. Apply database migrations:
   ```bash
   dotnet ef database update
   ```
4. Run the application:
   ```bash
   dotnet run
   ```
5. The API will be available at `https://localhost:<port>/`.

### API Documentation

- Swagger UI is available at `/swagger` when running in development mode.

## Usage

- Use the `/api/regions` endpoints to manage regions.
- Use the `/api/walks` endpoints to manage walks.
- Use the `/api/image/upload` endpoint to upload images.
- Use the `/api/auth` endpoints for authentication and token generation.

## Security

- JWT authentication is required for protected endpoints.
- Role-based authorization is supported (e.g., Reader, Writer roles).

## Logging

- Logs are written to the `Logs/` directory and the console.

## Notes

- The `Uploads/` directory is gitignored and used for storing uploaded images.
- For production, review security settings and connection strings.

## License

This project is licensed under the MIT License.
