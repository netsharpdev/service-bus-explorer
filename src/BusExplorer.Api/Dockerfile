# Use the SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /source

# Copy solution and project files to restore dependencies
COPY BusExplorer.sln .
COPY src/BusExplorer.Api/BusExplorer.Api.csproj ./src/BusExplorer.Api/
COPY src/BusExplorer.Data/BusExplorer.Data.csproj ./src/BusExplorer.Data/
COPY src/BusExplorer.Blazor/BusExplorer.Blazor.csproj ./src/BusExplorer.Blazor/
RUN dotnet restore BusExplorer.sln

# Copy the rest of the source code
COPY src/ ./src/

# Publish the API project
RUN dotnet publish "src/BusExplorer.Api/BusExplorer.Api.csproj" -c Release -o /app/publish --no-restore

# Use the ASP.NET runtime image for the final stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "BusExplorer.Api.dll"]
