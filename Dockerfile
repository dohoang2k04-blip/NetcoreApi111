FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

WORKDIR /src

COPY ["csmvc.csproj", "./"]

RUN dotnet restore "csmvc.csproj"

COPY . .

RUN dotnet publish "csmvc.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

WORKDIR /app

COPY --from=build /app/publish .

ENV ASPNETCORE_URLS="http://+:8080"

EXPOSE 8080

EXPOSE 443

ENTRYPOINT ["dotnet", "csmvc.dll"]