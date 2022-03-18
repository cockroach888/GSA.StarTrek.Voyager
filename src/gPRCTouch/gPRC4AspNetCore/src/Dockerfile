#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["gPRCTouch/gPRC4AspNetCore/src/gPRC4AspNetCore.csproj", "gPRCTouch/gPRC4AspNetCore/src/"]
RUN dotnet restore "gPRCTouch/gPRC4AspNetCore/src/gPRC4AspNetCore.csproj"
COPY . .
WORKDIR "/src/gPRCTouch/gPRC4AspNetCore/src"
RUN dotnet build "gPRC4AspNetCore.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "gPRC4AspNetCore.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "gPRC4AspNetCore.dll"]