#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Services/CurrencyRates.Loader/CurrencyRates.Loader.csproj", "Services/CurrencyRates.Loader/"]
RUN dotnet restore "Services/CurrencyRates.Loader/CurrencyRates.Loader.csproj"
COPY . .
WORKDIR "/src/Services/CurrencyRates.Loader"
RUN dotnet build "CurrencyRates.Loader.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CurrencyRates.Loader.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CurrencyRates.Loader.dll"]