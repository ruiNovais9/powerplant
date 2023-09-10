FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 8888
EXPOSE 443

# Use the official .NET Core SDK image as a build environment
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env
WORKDIR /app

# Copy the .csproj files and restore any dependencies (including runtime)
COPY ./PowerPlantAPI/*.csproj ./PowerPlantAPI/
COPY ./PowerPlants/*.csproj ./PowerPlants/
COPY ./PowerPlantsUnitTest/*.csproj ./PowerPlantsUnitTest/

RUN dotnet restore ./PowerPlantAPI/PowerPlantAPI.csproj
RUN dotnet restore ./PowerPlants/PowerPlants.csproj
RUN dotnet restore ./PowerPlantsUnitTest/PowerPlantsUnitTest.csproj

# Copy the remaining source code
COPY ./PowerPlantAPI/ ./PowerPlantAPI/
COPY ./PowerPlants/ ./PowerPlants/
COPY ./PowerPlantsUnitTest/ ./PowerPlantsUnitTest/

# Build the application
WORKDIR /app/PowerPlantAPI
RUN dotnet publish -c Release -o out

# Build the final runtime image
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build-env /app/PowerPlantAPI/out .
ENTRYPOINT ["dotnet", "PowerPlantAPI.dll"]