#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
EXPOSE 5004

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Services/CurrencyRates.CzBank.V2.Connector/CurrencyRates.CzBank.V2.Connector.csproj", "Services/CurrencyRates.CzBank.V2.Connector/"]
RUN dotnet restore "Services/CurrencyRates.CzBank.V2.Connector/CurrencyRates.CzBank.V2.Connector.csproj"
COPY . .
WORKDIR "/src/Services/CurrencyRates.CzBank.V2.Connector"
RUN dotnet build "CurrencyRates.CzBank.V2.Connector.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CurrencyRates.CzBank.V2.Connector.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CurrencyRates.CzBank.V2.Connector.dll"]