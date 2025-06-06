# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy just the project file and restore
COPY Paddle-Court-Microservice/Paddle-Court-Microservice.csproj Paddle-Court-Microservice/
COPY Shared-Contracts/Shared-Contracts.csproj Shared-Contracts/


RUN dotnet restore Paddle-Court-Microservice/Paddle-Court-Microservice.csproj

# Copy the rest of the source files
COPY Paddle-Court-Microservice/ Paddle-Court-Microservice/
COPY Shared-Contracts/ Shared-Contracts/

# Build the project
WORKDIR /src/Paddle-Court-Microservice
RUN dotnet build -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Paddle-Court-Microservice.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Paddle-Court-Microservice.dll"]