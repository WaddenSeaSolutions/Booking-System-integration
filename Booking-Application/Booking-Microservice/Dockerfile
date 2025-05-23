# Base runtime image for running the application
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# SDK image for building the application
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy csproj files
COPY Booking-Microservice/Booking-Microservice.csproj Booking-Microservice/
COPY Shared-Contracts/Shared-Contracts.csproj Shared-Contracts/

# Restore dependencies
RUN dotnet restore Booking-Microservice/Booking-Microservice.csproj

# Copy all source files
COPY Booking-Microservice/ Booking-Microservice/
COPY Shared-Contracts/ Shared-Contracts/

# Build the project
WORKDIR /src/Booking-Microservice
RUN dotnet build -c $BUILD_CONFIGURATION -o /app/build

# Publish the application
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Final runtime image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Booking-Microservice.dll"]
