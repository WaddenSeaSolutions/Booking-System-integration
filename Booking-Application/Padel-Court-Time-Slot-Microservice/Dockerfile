FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY Shared-Contracts/Shared-Contracts.csproj Shared-Contracts/
COPY ["Padel-Court-Time-Slot-Microservice/Padel-Court-Time-Slot-Microservice.csproj", "Padel-Court-Time-Slot-Microservice/"]
RUN dotnet restore "Padel-Court-Time-Slot-Microservice/Padel-Court-Time-Slot-Microservice.csproj"

COPY Shared-Contracts/ Shared-Contracts/
COPY . .
WORKDIR "/src/Padel-Court-Time-Slot-Microservice"
RUN dotnet build "Padel-Court-Time-Slot-Microservice.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Padel-Court-Time-Slot-Microservice.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Padel-Court-Time-Slot-Microservice.dll"]
