# Multi-stage Dockerfile for Blazor WebAssembly static hosting via Nginx
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj and restore dependencies
COPY ["LidyDecorApp.Web.csproj", "./"]
RUN dotnet restore "LidyDecorApp.Web.csproj"

# Copy the rest of the source code and publish
COPY . .
RUN dotnet publish "LidyDecorApp.Web.csproj" -c Release -o /app/publish

# Final stage: copy static files to Nginx
FROM nginx:alpine
WORKDIR /usr/share/nginx/html

# Copy published static files from build stage to Nginx html directory
COPY --from=build /app/publish/wwwroot .

# Copy custom Nginx configuration
COPY nginx.conf /etc/nginx/conf.d/default.conf

EXPOSE 80
ENTRYPOINT ["nginx", "-g", "daemon off;"]
