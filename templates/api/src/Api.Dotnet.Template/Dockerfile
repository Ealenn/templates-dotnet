FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
RUN curl -L https://raw.githubusercontent.com/Microsoft/artifacts-credprovider/master/helpers/installcredprovider.sh  | bash

COPY ["src/Api.Dotnet.Template/Api.Dotnet.Template.csproj", "src/Api.Dotnet.Template/"]
RUN dotnet restore "src/Api.Dotnet.Template/Api.Dotnet.Template.csproj"

COPY . .
WORKDIR "/src/src/Api.Dotnet.Template"
RUN dotnet build "Api.Dotnet.Template.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Api.Dotnet.Template.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Api.Dotnet.Template.dll"]
