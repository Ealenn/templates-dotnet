FROM mcr.microsoft.com/dotnet/core/sdk:3.1 as build
WORKDIR /src
COPY . .
RUN dotnet restore Ealen.Dotnet.Templates.csproj
RUN dotnet build Ealen.Dotnet.Templates.csproj -c Release --no-restore
RUN dotnet pack Ealen.Dotnet.Templates.csproj --output /nuget --no-build --verbosity Normal

FROM mcr.microsoft.com/dotnet/core/sdk:3.1
WORKDIR /app
COPY --from=build /nuget /nuget
RUN dotnet new -i /nuget/Ealen.Dotnet.Templates.1.0.0.nupkg

ENTRYPOINT [ "dotnet", "new" ]
CMD [ "list" ]