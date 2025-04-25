FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base

# Set environment variables and work directory
WORKDIR /app

EXPOSE 5000

ENV ASPNETCORE_URLS=http://+:5000
ENV ASPNETCORE_ENVIRONMENT=Development

# Check if the user with the specified ID already exists, and if not, create a new user
RUN if ! id -u $APP_UID > /dev/null 2>&1; then adduser --disabled-password --gecos '' --uid $APP_UID appuser; fi
USER $APP_UID

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy the project file and restore dependencies
COPY ["PeaceApp.API/PeaceApp.API.csproj", "PeaceApp.API/"]
RUN dotnet restore "PeaceApp.API/PeaceApp.API.csproj"

# Copy the remaining source code and build the application
COPY . .
WORKDIR "/src/PeaceApp.API"
RUN dotnet build "PeaceApp.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish the application
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "PeaceApp.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Use the base image for the final stage
FROM base AS final
WORKDIR /app

# Copy the published output from the build stage
COPY --from=publish /app/publish .

# Change ownership and permissions of the app directory
USER root
RUN chown -R $APP_UID:$APP_UID /app
RUN chmod -R 755 /app

# Switch to the custom user
USER $APP_UID

ENTRYPOINT ["dotnet", "PeaceApp.API.dll"]
