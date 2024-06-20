FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["PetSocialNetwork.Data.Migrator/PetSocialNetwork.Data.Migrator.csproj", "PetSocialNetwork.Data.Migrator/"]
RUN dotnet restore "PetSocialNetwork.Data.Migrator/PetSocialNetwork.Data.Migrator.csproj"
COPY . .
WORKDIR "/src/PetSocialNetwork.Data.Migrator"
RUN dotnet build "PetSocialNetwork.Data.Migrator.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "PetSocialNetwork.Data.Migrator.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PetSocialNetwork.Data.Migrator.dll"]
