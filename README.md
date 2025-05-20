# Product API

A simple REST API service that provides all available products of an e-shop and allows partial updates of a single product.

## Features

- List all available products
- Get a single product by ID
- Create new product
- Update product stock
- Swagger documentation

## Tech Stack

- ASP.NET Core Web API
- Entity Framework Core (with SQLite)
- Swagger (OpenAPI) for API documentation

---

## Prerequisites

- .NET 9 SDK
- A C#-compatible IDE like Visual Studio or VS Code

---

## How to Run the Application

1. **Clone the repository**:

   ```bash
   git clone https://github.com/matkobaran/ProductApi.git
   ```
2. **Build and run**:

   ```bash
   cd ProductApi
   dotnet restore
   dotnet ef database update
   dotnet run
   ```
3. **Open in browser***:

   ```bash
   http://localhost:5202/swagger
   ```

## Running Tests

This project includes unit tests to ensure the stability and correctness of the API.

### ▶️ Run Tests from Command Line

1. Open a terminal in the root of the solution.
2. Run the following command:
	```bash
	dotnet test
	```

## Author
Martin Baran