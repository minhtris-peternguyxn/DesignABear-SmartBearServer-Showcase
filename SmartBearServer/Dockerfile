# Build Stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy project files and restore dependencies
COPY ["SmartBearServer.csproj", "./"]
RUN dotnet restore "SmartBearServer.csproj"

# Copy everything else (including secrets and GCP keys)
COPY . .

# Build and publish
RUN dotnet build "SmartBearServer.csproj" -c Release -o /app/build
RUN dotnet publish "SmartBearServer.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Final Stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Expose ports
EXPOSE 7017

# Copy published files
COPY --from=build /app/publish .

# Environment variables for ASP.NET
ENV ASPNETCORE_URLS=http://+:7017
ENV ASPNETCORE_ENVIRONMENT=Production

# Ensure wwwroot exists for volume mapping
RUN mkdir -p wwwroot

# Entrypoint
ENTRYPOINT ["dotnet", "SmartBearServer.dll"]