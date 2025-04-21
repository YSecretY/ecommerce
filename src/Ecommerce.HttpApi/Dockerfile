FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Ecommerce.HttpApi/Ecommerce.HttpApi.csproj", "src/Ecommerce.HttpApi/"]
COPY ["shared/Ecommerce.CredentialProvider/Ecommerce.CredentialProvider.csproj", "shared/Ecommerce.CredentialProvider/"]
COPY ["src/Ecommerce.Infrastructure/Ecommerce.Infrastructure.csproj", "src/Ecommerce.Infrastructure/"]
COPY ["src/Ecommerce.Domain/Ecommerce.Domain.csproj", "src/Ecommerce.Domain/"]
COPY ["src/Ecommerce.Core/Ecommerce.Core.csproj", "src/Ecommerce.Core/"]
RUN dotnet restore "src/Ecommerce.HttpApi/Ecommerce.HttpApi.csproj"
COPY . .
WORKDIR "/src/src/Ecommerce.HttpApi"
RUN dotnet build "Ecommerce.HttpApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Ecommerce.HttpApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Ecommerce.HttpApi.dll"]
