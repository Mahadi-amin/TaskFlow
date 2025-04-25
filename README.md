# TaskFlow Project

## Overview
TaskFlow is a task management system built with .NET 9 and PostgreSQL, designed to streamline and manage workflow tasks. The application provides a simple user interface for managing tasks, their dependencies, and their statuses.

## Technologies Used
- **Backend**: .NET 9, ASP.NET Core MVC
- **ORM**: Entity Framework Core
- **Database**: PostgreSQL
- **Frontend**: Bootstrap
- **Other**: Swagger for API documentation

## Prerequisites
- .NET 9 SDK
- PostgreSQL

## Setting Up Development Environment

1. Clone the repository:


## Database Setup
Since the application is configured for automatic migrations, the database will automatically be updated with the latest schema upon application startup. Ensure that your PostgreSQL database is running and accessible, and the connection string in the `appsettings.json` is correctly set.

When you first run the application, it will automatically apply any pending migrations to the database.

## Adding a New Migration
If you need to add a new migration for schema changes, you can use the following command:

dotnet ef migrations add "NewMigration" --p TaskFlow.Infrastructure -s TaskFlow.Web --c ApplicationDbContext -o Data/Migrations


## Default Login Information

For testing purposes, you can log in with the following credentials:

- **Email**: admin@test.com
- **Password**: Admin@123


