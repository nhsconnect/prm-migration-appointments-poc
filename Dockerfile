#FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
#WORKDIR /app
#
## Copy csproj and restore as distinct layers
##COPY *.sln .
##COPY Api/*.csproj ./Api
##COPY GPConnectAdaptor/*.csproj ./GPConnectAdaptor
##RUN dotnet restore
#
## Copy everything else and build
#COPY . ./
#RUN dotnet publish -c Release -o out
#
## Build runtime image
#FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
#WORKDIR /app
#COPY --from=build-env /app/out .
#EXPOSE 5001
#ENTRYPOINT ["dotnet", "/out/App.dll"]
###

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /app

# copy csproj and restore as distinct layers
COPY *.sln .
COPY Api/*.csproj ./Api/
COPY GPConnectAdaptor/*.csproj ./GPConnectAdaptor/
COPY GPConnectAdaptorTests/*.csproj ./GPConnectAdaptorTests/
RUN dotnet restore

# copy everything else and build app
COPY Api/. ./Api/
COPY GPConnectAdaptor/. ./GPConnectAdaptor/
WORKDIR /app
RUN dotnet publish -c Release -o out --no-restore


FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS runtime
WORKDIR /app
COPY --from=build /app/out ./
EXPOSE 5001
ENTRYPOINT ["dotnet", "Api.dll"]