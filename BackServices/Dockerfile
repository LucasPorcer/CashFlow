#Arquivo de configura��o para rodar a webapi via docker.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 4000

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Cashflow.Api/Cashflow.Api.csproj", "Cashflow.Api/"]
COPY ["Cashflow.Domain/Cashflow.Domain.csproj", "Cashflow.Domain/"]
COPY ["Cashflow.Application/Cashflow.Application.csproj", "Cashflow.Application/"]
COPY ["Cashflow.Infra/Cashflow.Infra.csproj", "Cashflow.Infra/"]
COPY ["Cashflow.Infra.IoC/Cashflow.Infra.IoC.csproj", "Cashflow.Infra.IoC/"]

RUN dotnet restore "./Cashflow.Api/Cashflow.Api.csproj"
RUN dotnet restore "./Cashflow.Domain/Cashflow.Domain.csproj"
RUN dotnet restore "./Cashflow.Application/Cashflow.Application.csproj"
RUN dotnet restore "./Cashflow.Infra/Cashflow.Infra.csproj"
RUN dotnet restore "./Cashflow.Infra.IoC/Cashflow.Infra.IoC.csproj"

COPY . .
WORKDIR "/src/Cashflow.Api"
RUN dotnet build "Cashflow.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Cashflow.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Cashflow.Api.dll"]
