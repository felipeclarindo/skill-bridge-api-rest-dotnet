# SDK para build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copia apenas a solução e os csproj antes (para otimizar cache)
COPY Src/WebApi/WebApi.csproj Src/WebApi/
COPY Src/Tests/Tests.csproj Src/Tests/
COPY skill-bridge-api-rest.sln ./

RUN dotnet restore

# Agora copia o resto
COPY . .

RUN dotnet publish Src/WebApi/WebApi.csproj -c Release -o /app/publish

# Runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app

COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "WebApi.dll"]
