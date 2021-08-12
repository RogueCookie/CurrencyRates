#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Services/CurrencyRates.CzBank.Connector/CurrencyRates.CzBank.Connector.csproj", "Services/CurrencyRates.CzBank.Connector/"]
RUN dotnet restore "Services/CurrencyRates.CzBank.Connector/CurrencyRates.CzBank.Connector.csproj"
COPY . .
WORKDIR "/src/Services/CurrencyRates.CzBank.Connector"
RUN dotnet build "CurrencyRates.CzBank.Connector.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CurrencyRates.CzBank.Connector.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CurrencyRates.CzBank.Connector.dll"]