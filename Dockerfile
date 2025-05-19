# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy only the .csproj first and restore dependencies
COPY UrlShortener.Api/UrlShortener.Api.csproj UrlShortener.Api/
RUN dotnet restore UrlShortener.Api/UrlShortener.Api.csproj

# Copy the rest of the source
COPY UrlShortener.Api/ UrlShortener.Api/

# Build and publish the app
WORKDIR /src/UrlShortener.Api
RUN dotnet publish -c Release -o /app/publish
RUN ls -al /app/publish

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Copy published output from build stage
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "UrlShortener.Api.dll"]
EXPOSE 80