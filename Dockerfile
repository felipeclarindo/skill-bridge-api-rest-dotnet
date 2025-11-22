# ===========================
# STAGE 1 — BUILD
# ===========================
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copia apenas csproj para usar cache
COPY Src/WebApi/WebApi.csproj Src/WebApi/
COPY Src/Tests/Tests.csproj Src/Tests/
COPY skill-bridge-api-rest.sln ./

RUN dotnet restore

# Copia restante do código
COPY . .

# Publica o WebApi
RUN dotnet publish Src/WebApi/WebApi.csproj -c Release -o /publish


# ===========================
# STAGE 2 — RUNTIME
# ===========================
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app

# Copia publicação do build
COPY --from=build /publish .

# Render injeta a variável PORT automaticamente
ENV ASPNETCORE_URLS=http://+:$PORT

# Apenas para rodar localmente (Render ignora EXPOSE)
EXPOSE 8080

ENTRYPOINT ["dotnet", "WebApi.dll"]
