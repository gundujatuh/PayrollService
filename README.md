# PayrollService (.NET 8) - Sample for Technical Assessment (SQL Server)

## Overview
Simple payroll microservice using:
- .NET 8
- EF Core with SQL Server
- GET /api/employee
- GET /api/employee/{id}
- POST /api/payroll/calculate
- /health endpoint
- Config via `appsettings.json`
- Basic logging and global error handling
- Dockerized (NOT IMPLEMENTED YETTT)
- Swagger UI enabled

## Requirements
- .NET 8 SDK
- Docker (NOT IMPLEMENTED YETTT)
- (Optional) EF Core tools for migrations: `dotnet tool install --global dotnet-ef`

## HOW TO RUN
- git clone
- dotnet restore
- change connection strings in appsettings.json


(NOT IMPLEMENTED YETTT)
(NOT IMPLEMENTED YETTT)
(NOT IMPLEMENTED YETTT)
## Local development with Docker (recommended)
This setup runs SQL Server in a container and the app in another container using Docker network.

1. Build the app image:
```bash
docker build -t payrollservice:local .
```

2. Run SQL Server container:
```bash
docker run -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=Your_password123' --name sqlserver -p 1433:1433 -d mcr.microsoft.com/mssql/server:2022-latest
```

3. Run the app container (ensure it can connect to the SQL Server container; using default connection string points to host `sqlserver`):
```bash
docker network create payroll-net || true
docker run --network payroll-net --name payrollservice -e ASPNETCORE_ENVIRONMENT=Production -p 8080:80 -d payrollservice:local
```

> Note: If running SQL Server locally (not in a container), update `appsettings.json` ConnectionStrings accordingly.

## Apply EF Core Migrations (optional)
From project directory:
```bash
dotnet tool install --global dotnet-ef
dotnet ef migrations add InitialCreate
dotnet ef database update
```

## Run locally (without Docker)
```bash
dotnet build
dotnet run
# app will be available on http://localhost:5000 (or port shown in logs)
```

## Build Docker image
```bash
docker build -t payrollservice:local .
docker run -p 8080:80 payrollservice:local
# access http://localhost:8080/swagger
```

## Example requests
GET /api/employee
POST /api/payroll/calculate

## Notes / Security
- Do NOT hardcode production secrets. Use environment variables or secret manager.
- The example default SQL Server password is for local demo only.
