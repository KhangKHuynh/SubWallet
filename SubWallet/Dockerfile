# Use official .NET 8 ASP.NET runtime image as base
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Use SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
# Copy csproj and restore dependencies
COPY *.csproj ./
RUN dotnet restore
# Copy the rest of the source code and build
COPY . ./
RUN dotnet publish SubWallet.csproj -c Release -o ./publish /p:UseAppHost=false
# Final stage: build runtime image
FROM base AS final
WORKDIR /app
COPY --from=build /src/publish .
ENTRYPOINT ["dotnet", "SubWallet.dll"]


