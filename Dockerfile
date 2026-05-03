FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY OrderService/OrderService.API/OrderService.API.csproj OrderService/OrderService.API/
COPY OrderService/OrderService.Application/OrderService.Application.csproj OrderService/OrderService.Application/
COPY OrderService/OrderService.Infrastructure/OrderService.Infrastructure.csproj OrderService/OrderService.Infrastructure/
COPY OrderService/OrderService.Domain/OrderService.Domain.csproj OrderService/OrderService.Domain/
COPY SharedContracts/SharedContracts.csproj SharedContracts/

RUN dotnet restore OrderService/OrderService.API/OrderService.API.csproj

COPY OrderService/ OrderService/
COPY SharedContracts/ SharedContracts/

RUN dotnet publish OrderService/OrderService.API/OrderService.API.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "OrderService.API.dll"]
