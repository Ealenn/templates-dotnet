FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["src/Dotnet.Cqrs.Api/Dotnet.Cqrs.Api.csproj", "src/Dotnet.Cqrs.Api/"]
RUN dotnet restore "src/Dotnet.Cqrs.Api/Dotnet.Cqrs.Api.csproj"
COPY . .
WORKDIR "/src/src/Dotnet.Cqrs.Api"
RUN dotnet build "Dotnet.Cqrs.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Dotnet.Cqrs.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Dotnet.Cqrs.Api.dll"]