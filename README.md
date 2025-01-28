# ProductManager API

## Project Overview
ProductManager is a .NET Core Web API application for managing product information. It provides endpoints for retrieving, updating, and managing products with support for versioned APIs.

## Features
- API Versioning (v1 and v2)
- Pagination support
- Product management endpoints
- Swagger documentation
- Unit testing
- Integration testing

## Prerequisites
- .NET 8.0 SDK or later
- Visual Studio 2022 or Visual Studio Code
- SQL Server (or compatible database)

## Setup and Installation

### 1. Clone the Repository
```bash
git clone https://github.com/yourusername/ProductManager.git
cd ProductManager
```

### 2. Database Configuration
- Ensure SQL Server is installed
- Update the connection string in `appsettings.json` to match your database configuration

```json
{
  "ConnectionStrings": {
    "EshopContext": ""
  }
}
```

### 3. Database Migrations
Run the following commands to create and update the database:
```bash
dotnet ef database update
```

## Running the Application

### Using Visual Studio
1. Open `Eshop.sln` in Visual Studio
2. Press F5 or click "Run" to start the application

### Using Command Line
```bash
cd ProductManager
dotnet run
```

The application will start and be accessible at `http://localhost:5116`

## Swagger Documentation
Swagger UI is available at `/swagger` endpoint for exploring and testing API endpoints.

## API Endpoints

### V1 Endpoints
- `GET /api/v1/products`: Retrieve all products
- `GET /api/v1/product/{id}`: Retrieve a specific product
- `PATCH /api/v1/product/update/description`: Update product description

### V2 Endpoints
- `GET /api/v2/products/{pageIndex}/{pageSize}`: Retrieve paginated products

## Running Unit Tests
To run unit tests, use the following command:
```bash
cd cd ProductManager.Tests
dotnet test
```

## Postman Collection
A Postman collection is provided for API testing:
- `ProductManager.postman_collection.json`: API endpoint tests

## Technologies
- .NET 8.0
- Entity Framework Core
- AutoMapper
- Swagger
- Postman for API testing
