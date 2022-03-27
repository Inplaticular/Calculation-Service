# 1. Build application in image
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

WORKDIR /src

COPY ["src/Calculation-Service.WebAPI/Calculation-Service.WebAPI.csproj", "Calculation-Service.WebAPI/"]
COPY ["src/Calculation-Service.Infrastructure/Calculation-Service.Infrastructure.csproj", "Calculation-Service.Infrastructure/"]
COPY ["src/Calculation-Service.Core/Calculation-Service.Core.csproj", "Calculation-Service.Core/"]

RUN dotnet restore "Calculation-Service.WebAPI/Calculation-Service.WebAPI.csproj"

COPY ./src .
WORKDIR "/src/Calculation-Service.WebAPI"

RUN dotnet build "Calculation-Service.WebAPI.csproj" -c Release -o /app/build

# 2. Publish built application in image
FROM build AS publish
RUN dotnet publish "Calculation-Service.WebAPI.csproj" -c Release -o /app/publish

# 3. Take published version
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS final
EXPOSE 80
WORKDIR /app
COPY --from=publish /app/publish .

