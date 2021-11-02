#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["RouteComputationService.csproj", "."]
RUN dotnet restore "./RouteComputationService.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "RouteComputationService.csproj" -c Release -o /app/build
HEALTHCHECK --interval=5s --timeout=3s CMD curl --fail http://localhost:5000/health || exit 1

FROM build AS publish
RUN dotnet publish "RouteComputationService.csproj" -c Release -o /app/publish
HEALTHCHECK --interval=5s --timeout=3s CMD curl --fail http://localhost:5000/health || exit 1

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
HEALTHCHECK --interval=5s --timeout=3s CMD curl --fail http://localhost:5000/health || exit 1
ENTRYPOINT ["dotnet", "RouteComputationService.dll"]