#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["OrleansSilo/OrleansSilo.csproj", "OrleansSilo/"]
RUN dotnet restore "OrleansSilo/OrleansSilo.csproj"
COPY . .
WORKDIR "/src/OrleansSilo"
RUN dotnet build "OrleansSilo.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OrleansSilo.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OrleansSilo.dll"]