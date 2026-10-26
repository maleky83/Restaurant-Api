# Restaurant API

A RESTful Web API built with **ASP.NET Core 10** for managing restaurants, dishes, and users, following **Clean Architecture** and modern backend development practices.

## Features

- **Clean Architecture** — Separation of concerns and maintainable project structure.
- **RESTful Web API** — REST principles and HTTP conventions.
- **Entity Framework Core** — Database access with Fluent API configuration.
- **CQRS** — Separation of command and query responsibilities.
- **Mediator Pattern** — Decoupled request handling using the Mediator pattern.
- **Authentication & Authorization** — Secure access control.
- **ASP.NET Core Identity** — User and role management.
- **Dependency Injection** — Loose coupling and testability.
- **AutoMapper** — Object-to-object mapping.
- **FluentValidation** — Request and input validation.
- **Serilog** — Structured application logging.
- **Global Exception Handling** — Centralized error handling middleware.
- **Pagination** — Efficient retrieval of large datasets.
- **Seed Data** — Initial database data for development and testing.
- **Swagger / OpenAPI** — Interactive API documentation.
- **Postman** — API testing and request collections.
- **Unit Testing** — Testing individual components in isolation.
- **Integration Testing** — Testing API endpoints and application components together.

## Domain Entities

The application includes the following core entities:

- **User** — Application users and their associated identity information.
- **Restaurant** — Restaurant information and related data.
- **Dish** — Dishes associated with restaurants.

## Tech Stack

- C#
- ASP.NET Core 10
- Entity Framework Core
- ASP.NET Core Identity
- CQRS and Mediator Pattern
- AutoMapper
- FluentValidation
- Serilog
- Swagger / OpenAPI
- Postman
- Unit and Integration Testing

## Architecture

The project follows Clean Architecture principles to separate business logic from infrastructure and presentation concerns.

The architecture aims to provide:

- Separation of concerns
- Maintainability and scalability
- Testability
- Reduced coupling between application components
- Clear boundaries between application layers

## Getting Started

### Prerequisites

- .NET 10 SDK
- A supported database provider configured for Entity Framework Core
- Visual Studio, JetBrains Rider, or Visual Studio Code

### Installation

1. Clone the repository:

   ```bash
   git clone <repository-url>
   ```

2. Navigate to the project directory:

   ```bash
   cd Restaurant-Api
   ```

3. Configure the database connection string in the appropriate configuration file or user secrets.

4. Apply Entity Framework Core migrations:

   ```bash
   dotnet ef database update
   ```

5. Run the application:

   ```bash
   dotnet run --project <API-project-path>
   ```

6. Open the Swagger UI URL displayed by the application, if Swagger is enabled in the current environment.

## Testing

Run the test suite using:

```bash
dotnet test
```

The project includes unit and integration tests to help verify application behavior and API functionality.

## API Documentation

API endpoints can be explored through Swagger / OpenAPI.

Postman can also be used to send requests, validate responses, and test authentication and authorization flows.

## Project Status

This project is developed as a backend learning project to practice modern ASP.NET Core Web API development, Clean Architecture, and software testing.

## License

No license has been specified yet.
