FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Soccer.EndPoint/Soccer.EndPoint.csproj", "Soccer.EndPoint/"]
RUN dotnet restore "Soccer.EndPoint/Soccer.EndPoint.csproj"
COPY . .
WORKDIR "/src/Soccer.EndPoint"
RUN dotnet build "Soccer.EndPoint.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Soccer.EndPoint.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Soccer.EndPoint.dll"]
