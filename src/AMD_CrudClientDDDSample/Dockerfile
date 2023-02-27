FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY . ./

# Copy everything else and build
RUN dotnet publish "AMD_CrudClientDDDSample" -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build-env /app/out .

ENV ASPNETCORE_ENVIRONMENT="Development"

EXPOSE 80
EXPOSE 443

ENTRYPOINT ["dotnet", "AMD_CrudClientDDDSample.Services.dll"]