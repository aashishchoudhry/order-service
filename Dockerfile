# Stage 1 — Build
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY OrderService.API/OrderService.API.csproj OrderService.API/
COPY OrderService.Application/OrderService.Application.csproj OrderService.Application/
COPY OrderService.Infrastructure/OrderService.Infrastructure.csproj OrderService.Infrastructure/
COPY OrderService.Domain/OrderService.Domain.csproj OrderService.Domain/

RUN dotnet restore OrderService.API/OrderService.API.csproj

COPY . .

RUN dotnet publish OrderService.API/OrderService.API.csproj -c Release -o /app/publish

# Stage 2 — Runtime
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "OrderService.API.dll"]