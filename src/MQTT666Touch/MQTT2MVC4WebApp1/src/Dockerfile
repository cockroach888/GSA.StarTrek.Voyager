#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0.4 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0.4 AS build
WORKDIR /src
COPY ["MQTT666Touch/MQTT2MVC4WebApp1/src/MQTT2MVC4WebApp1.csproj", "MQTT666Touch/MQTT2MVC4WebApp1/src/"]
RUN dotnet restore "MQTT666Touch/MQTT2MVC4WebApp1/src/MQTT2MVC4WebApp1.csproj"
COPY . .
WORKDIR "/src/MQTT666Touch/MQTT2MVC4WebApp1/src"
RUN dotnet build "MQTT2MVC4WebApp1.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MQTT2MVC4WebApp1.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MQTT2MVC4WebApp1.dll"]