#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

ENV DOCKER_ConnectionString="Server=172.17.0.3;Database=N5Challenge;User Id=SA;Password=<YourStrong@Passw0rd>;MultipleActiveResultSets=true;TrustServerCertificate=true;"

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["LdelossantosN5.WebApi/LdelossantosN5.WebApi.csproj", "LdelossantosN5.WebApi/"]
RUN dotnet restore "./LdelossantosN5.WebApi/LdelossantosN5.WebApi.csproj"
COPY . .
WORKDIR "/src/LdelossantosN5.WebApi"
RUN dotnet build "./LdelossantosN5.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./LdelossantosN5.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LdelossantosN5.WebApi.dll"]