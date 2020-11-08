#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim-arm32v7 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
RUN apt-get update && apt-get install -y --no-install-recommends \
    libc6-dev \
    libgdiplus

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster-arm32v7 AS build
WORKDIR /src
COPY ["app/Ray.Web.Api/Ray.Web.Api.csproj", "app/Ray.Web.Api/"]
RUN dotnet restore "app/Ray.Web.Api/Ray.Web.Api.csproj"
COPY . .
WORKDIR "/src/app/Ray.Web.Api"
RUN dotnet build "Ray.Web.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Ray.Web.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Ray.Web.Api.dll"]