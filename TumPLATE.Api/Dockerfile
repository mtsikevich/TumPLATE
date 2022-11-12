FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["TumPLATE.Api/TumPLATE.Api.csproj", "TumPLATE.Api/"]
RUN dotnet restore "TumPLATE.Api/TumPLATE.Api.csproj"
COPY . .
WORKDIR "/src/TumPLATE.Api"
RUN dotnet build "TumPLATE.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TumPLATE.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TumPLATE.Api.dll"]
