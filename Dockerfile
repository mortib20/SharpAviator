FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Aviator.Main/Aviator.Main.csproj", "Aviator.Main/"]
RUN dotnet restore "Aviator.Main/Aviator.Main.csproj"
COPY . .
WORKDIR "/src/Aviator.Main"
RUN dotnet build "Aviator.Main.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Aviator.Main.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
RUN dotnet tool install --global dotnet-ef
RUN dotnet ef database update
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Aviator.Main.dll"]
