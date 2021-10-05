FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY RouteComputationService.csproj .
RUN dotnet restore
COPY . .
RUN dotnet publish -c release -o /app

HEALTHCHECK CMD curl --fail http://localhost:5000/health || exit

FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "RouteComputationService.dll"]
