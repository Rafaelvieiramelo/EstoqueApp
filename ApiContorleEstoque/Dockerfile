# Multi-stage Dockerfile for ASP.NET Core Web API
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081
RUN apt-get update && apt-get install -y --no-install-recommends libreoffice fontconfig && rm -rf /var/lib/apt/lists/*

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy project files first to leverage Docker cache for restore
COPY ["ApiContorleEstoque/LidyDecorApp.API.csproj", "ApiContorleEstoque/"]
COPY ["EstoqueApp.Application/LidyDecorApp.Application.csproj", "EstoqueApp.Application/"]
COPY ["EstoqueApp.Infrastructure/LidyDecorApp.Infrastructure.csproj", "EstoqueApp.Infrastructure/"]
COPY ["EstoqueApp.Shared/LidyDecorApp.Shared.csproj", "EstoqueApp.Shared/"]
COPY ["EstoqueApp.Domain/LidyDecorApp.Domain.csproj", "EstoqueApp.Domain/"]

RUN dotnet restore "ApiContorleEstoque/LidyDecorApp.API.csproj"

# Copy the remaining source files and compile
COPY . .
WORKDIR "/src/ApiContorleEstoque"
RUN dotnet build "LidyDecorApp.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "LidyDecorApp.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LidyDecorApp.API.dll"]
