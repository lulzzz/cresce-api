FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env

#Set workdir
WORKDIR /app

#Copy csproj and restore as distinct layers
COPY . ./

ENV DOTNET_SYSTEM_NET_HTTP_USESOCKETSHTTPHANDLER=0

RUN dotnet restore ./Cresce.Api.sln

RUN dotnet publish -c Release -o out

#Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS runtime

WORKDIR /app

COPY --from=build-env /app/out .

ENTRYPOINT ["dotnet", "Cresce.Api.dll"]
