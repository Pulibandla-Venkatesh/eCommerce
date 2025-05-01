# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Install .NET Framework Developer Pack (Needed for .NET 4.7.2)
RUN apt-get update && apt-get install -y wget \
    && wget -q https://download.visualstudio.microsoft.com/download/pr/3d6b37dc-c598-4cd2-b4ab-924aeaf5e741/1c033e2298a0d5704b5c6e46e3a3a4c1/dotnet-hosting-4.7.2.tar.gz \
    && tar -xvf dotnet-hosting-4.7.2.tar.gz -C /usr/share/dotnet/

COPY ["eCommerce/eCommerce.csproj", "eCommerce/"]
COPY ["eCommerceDataLayer/eCommerceDataLayer.csproj", "eCommerceDataLayer/"]
RUN dotnet restore "./eCommerce/eCommerce.csproj"
COPY . .
WORKDIR "/src/eCommerce"
RUN dotnet build "./eCommerce.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./eCommerce.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "eCommerce.dll"]