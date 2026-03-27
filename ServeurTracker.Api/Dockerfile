# ÉTAPE 1 : L'environnement de construction (Le SDK)
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# On copie d'abord uniquement le fichier projet pour optimiser la mise en cache de Docker
COPY ["ServeurTracker.Api.csproj", "./"]
# On télécharge toutes les dépendances (les paquets NuGet comme Entity Framework)
RUN dotnet restore "ServeurTracker.Api.csproj"

# Ensuite, on copie tout le reste de ton code source
COPY . .
WORKDIR "/src/"

# On compile l'application en mode "Release" (optimisé pour la production)
RUN dotnet publish "ServeurTracker.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false


# ÉTAPE 2 : L'environnement d'exécution (Le Runtime allégé)
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app

# On ouvre le port 8080 (le port standard des conteneurs web modernes)
EXPOSE 8080
ENV ASPNETCORE_URLS=http://0.0.0.0:8080

# On récupère les fichiers compilés de l'ÉTAPE 1 (build)
COPY --from=build /app/publish .

# On dit à Docker quelle commande lancer quand le conteneur démarre
ENTRYPOINT ["dotnet", "ServeurTracker.Api.dll"]