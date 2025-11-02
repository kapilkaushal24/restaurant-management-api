# Dockerfile for Restaurant Management API

# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy solution file
COPY ["RestaurantManagement.sln", "./"]

# Copy project files
COPY ["src/RestaurantManagement.Domain/RestaurantManagement.Domain.csproj", "src/RestaurantManagement.Domain/"]
COPY ["src/RestaurantManagement.Application/RestaurantManagement.Application.csproj", "src/RestaurantManagement.Application/"]
COPY ["src/RestaurantManagement.Infrastructure/RestaurantManagement.Infrastructure.csproj", "src/RestaurantManagement.Infrastructure/"]
COPY ["src/RestaurantManagement.API/RestaurantManagement.API.csproj", "src/RestaurantManagement.API/"]

# Restore dependencies
RUN dotnet restore "src/RestaurantManagement.API/RestaurantManagement.API.csproj"

# Copy everything else
COPY . .

# Build the application
WORKDIR "/src/src/RestaurantManagement.API"
RUN dotnet build "RestaurantManagement.API.csproj" -c Release -o /app/build

# Stage 2: Publish
FROM build AS publish
RUN dotnet publish "RestaurantManagement.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Stage 3: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app

# Create a non-root user
RUN groupadd -r appuser && useradd -r -g appuser appuser

# Copy published files
COPY --from=publish /app/publish .

# Create directory for SQLite database
RUN mkdir -p /app/data && chown -R appuser:appuser /app/data

# Set ownership
RUN chown -R appuser:appuser /app

# Switch to non-root user
USER appuser

# Expose port
EXPOSE 8080

# Health check
HEALTHCHECK --interval=30s --timeout=3s --start-period=5s --retries=3 \
  CMD curl -f http://localhost:8080/health || exit 1

# Set environment variables
ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Production
ENV PORT=8080

# Entry point
ENTRYPOINT ["dotnet", "RestaurantManagement.API.dll"]
