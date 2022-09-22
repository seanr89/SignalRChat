FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app

# It's important to keep lines from here down to "COPY . ." identical in all Dockerfiles
# # to take advantage of Docker's build cache, to speed up local container builds
#COPY "../ApacheFileProcessing.sln" "ApacheFileProcessing.sln"

# Copy csproj and restore as distinct layers
COPY *.csproj ./

COPY ../* ./

RUN dotnet restore

# Copy everything else and build
COPY . .
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "SignalServer.dll"]