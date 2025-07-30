# Use the SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /source

# Copy the solution file and all project files, then restore dependencies
COPY *.sln .
COPY src/BusExplorer.Blazor/*.csproj ./src/BusExplorer.Blazor/
COPY src/BusExplorer.Api/*.csproj ./src/BusExplorer.Api/
COPY src/BusExplorer.Data/*.csproj ./src/BusExplorer.Data/
RUN dotnet restore

# Copy the rest of the source code
COPY . .

# Build and publish the Blazor project
RUN dotnet publish "./src/BusExplorer.Blazor/BusExplorer.Blazor.csproj" -c Release -o /app/publish

# Use the ASP.NET runtime image for the final stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "BusExplorer.Blazor.dll"]