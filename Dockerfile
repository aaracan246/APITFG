# Etapa 1: build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar el código y restaurar dependencias
COPY . .
RUN dotnet restore

# Publicar la app
RUN dotnet publish -c Release -o /app/publish

# Etapa 2: runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

# Reemplaza StatsApi.dll si tu proyecto tiene otro nombre
ENTRYPOINT ["dotnet", "StatsApi.dll"]
