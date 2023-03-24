FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["docs/Architecture/Architecture.csproj", "docs/Architecture/"]
RUN dotnet restore "docs/Architecture/Architecture.csproj"
COPY . .
WORKDIR "/src/docs/Architecture"
RUN dotnet build "Architecture.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Architecture.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Architecture.dll"]
