FROM beesona/aspnet3.1-db2:latest AS base

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY WebApi.Data/WebApi.Data.csproj WebApi.Data/
COPY WebApi.Domain/WebApi.Domain.csproj WebApi.Domain/
COPY WebApi.Service/WebApi.Service.csproj WebApi.Service/
RUN dotnet restore "WebApi.Data"
RUN dotnet restore "WebApi.Domain"
RUN dotnet restore "WebApi.Service"

COPY . .
RUN dotnet build "WebApi.Service" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebApi.Service" -c Release -o /app/publish

FROM base AS final
COPY --from=publish /app/publish .

COPY db2consv_is.lic ./clidriver/license/db2consv_is.lic

ENTRYPOINT ["dotnet", "WebApi.Service.dll"]