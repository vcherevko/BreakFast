FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["BreakFast.Ordering.API/BreakFast.Ordering.API.csproj", "BreakFast.Ordering.API/"]
COPY ["BreakFast.Ordering.Domain/BreakFast.Ordering.Domain.csproj", "BreakFast.Ordering.Domain/"]
COPY ["BreakFast.Ordering.Infrastructure/BreakFast.Ordering.Infrastructure.csproj", "BreakFast.Ordering.Infrastructure/"]
RUN dotnet restore "BreakFast.Ordering.API/BreakFast.Ordering.API.csproj"
COPY . .
WORKDIR "/src/BreakFast.Ordering.API"
RUN dotnet build "BreakFast.Ordering.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "BreakFast.Ordering.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BreakFast.Ordering.API.dll"]
