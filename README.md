# TaskFlow Project

## Overview
TaskFlow is a task management system built with .NET 9 and PostgreSQL, designed to streamline and manage workflow tasks. The application provides a simple user interface for managing tasks, their dependencies, and their statuses.

## Technologies Used
- **Backend**: .NET 9, ASP.NET Core MVC
- **ORM**: Entity Framework Core
- **Database**: PostgreSQL
- **Frontend**: Bootstrap (if used)
- **Other**: Swagger for API documentation (if used)

## Prerequisites
- .NET 9 SDK
- PostgreSQL

## Setting Up Development Environment
1. Clone the repository:
    ```bash
    git clone <repository-url>
    ```
2. Install dependencies:
    ```bash
    dotnet restore
    ```

## Database Setup
Since the application is configured for automatic migrations, the database will automatically be updated with the latest schema upon application startup. Ensure that your PostgreSQL database is running and accessible, and the connection string in the `appsettings.json` is correctly set.

When you first run the application, it will automatically apply any pending migrations to the database.

## Running the Application
1. To run the application locally:
    ```bash
    dotnet run --project TaskFlow.Web
    ```
2. Access the application via:
    ```bash
    http://localhost:<port>
    ```