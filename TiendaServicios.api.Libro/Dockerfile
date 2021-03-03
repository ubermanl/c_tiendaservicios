#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["TiendaServicios.api.Libro/TiendaServicios.api.Libro.csproj", "TiendaServicios.api.Libro/"]
RUN dotnet restore "TiendaServicios.api.Libro/TiendaServicios.api.Libro.csproj"
COPY . .
WORKDIR "/src/TiendaServicios.api.Libro"
RUN dotnet build "TiendaServicios.api.Libro.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TiendaServicios.api.Libro.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TiendaServicios.api.Libro.dll"]
