FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build

RUN curl --silent --location https://deb.nodesource.com/setup_10.x | bash -
RUN apt-get install --yes nodejs

WORKDIR /src
COPY . .

RUN dotnet restore "Cinema/Cinema.csproj"

RUN dotnet publish "Cinema/Cinema.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim

EXPOSE 80

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "Cinema.dll"]
