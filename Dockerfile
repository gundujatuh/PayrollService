# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY *.sln ./
COPY *.csproj ./
COPY . .
RUN dotnet restore
RUN dotnet publish -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
EXPOSE 80
COPY --from=build /app/publish .

ENV ConnectionStrings__DefaultConnection="Server=host.docker.internal\\SQLEXPRESS01;Database=DB_Rio_Payroll;User Id=superuser;Password=inipassword123!;TrustServerCertificate=True;"

ENTRYPOINT ["dotnet", "PayrollService.dll"]