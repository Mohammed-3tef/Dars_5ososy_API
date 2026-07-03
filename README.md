# Dars 5ososy API

Backend REST API for the Dars 5ososy platform, built with ASP.NET Core and organized as a layered solution. The system supports user authentication, tutor and student profiles, subject management, availability scheduling, bookings, reviews, favorites, addresses, and image uploads.

## Overview

The API is versioned, documented with Swagger, secured with JWT authentication, and backed by Entity Framework Core with ASP.NET Core Identity. The solution is split into separate projects to keep the domain, application, infrastructure, and shared concerns isolated.

## Technology Stack

- .NET 8 / ASP.NET Core Web API
- Entity Framework Core
- ASP.NET Core Identity
- JWT Bearer Authentication
- Swagger / OpenAPI
- AutoMapper
- API Versioning

## Solution Structure

- `Dars_5ososy_API` - API host, controllers, configuration, and dependency injection
- `Dars_5ososy_API.Application` - DTOs, services, and mapping profiles
- `Dars_5ososy_API.Domain` - entities and domain models
- `Dars_5ososy_API.Infrastructure` - database context, repositories, migrations, and seeders
- `Dars_5ososy_API.Shared` - shared helpers and response models

## Core Modules

- Authentication and token management
- Users, teachers, and students
- Subjects and teacher subjects
- Availability slots and bookings
- Education systems, stages, governorates, provinces, and areas
- Favorites and reviews
- Image upload and retrieval

## Prerequisites

- .NET 8 SDK
- SQL Server
- Visual Studio 2022 or VS Code with C# tooling

## API Conventions

- Base route: `api/v{version}`
- Responses are wrapped with `ApiResponse<T>`
- API versioning is enabled through URL segment, query string, header, and media type readers
- Swagger documents the active API version(s)

## Development Notes

- Controllers are thin and delegate business logic to services
- Services handle mapping and orchestration
- Repositories encapsulate data access through `AppDbContext`
- DTOs are used for request and response contracts

## Build Status

The current solution builds successfully with .NET 8.

## Additional Documentation & Wiki

For more detailed information, guides, and deeper technical breakdowns, please visit our **[Project Wiki](https://github.com/Mohammed-3tef/Dars_5ososy_API/wiki)**.

You can explore the following dedicated pages:
* **[Wiki Home Page](https://github.com/Mohammed-3tef/Dars_5ososy_API/wiki)**: A high-level overview, getting started setup guides, and project onboarding documentation.
* **[Controllers & Routing Guide](https://github.com/Mohammed-3tef/Dars_5ososy_API/wiki/Controllers)**: Detailed breakdown of the API endpoints, route structure (`api/v{version}`), controller responsibilities, and versioning rules.