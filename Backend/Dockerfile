FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS net_restore
WORKDIR /src
COPY ["Backend/Backend.csproj","Backend/"]
WORKDIR "/src/Backend"
RUN dotnet restore "Backend.csproj"
COPY ["Backend/","."]

FROM net_restore AS net_build
RUN dotnet build "Backend.csproj" -c Release -o /app/build

FROM net_build AS net_publish
RUN dotnet publish "Backend.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM node:slim as node_install
WORKDIR /src
COPY ["Frontend/package.json","Frontend/"] 
COPY ["Frontend/package-lock.json","Frontend/"] 
WORKDIR "/src/Frontend"
RUN npm install
COPY ["Frontend/","."]

FROM node_install as node_build
RUN npm run build -- --configuration production --output-path /app/publish/wwwroot

FROM base AS final
WORKDIR /app
COPY --from=net_publish /app/publish .
COPY --from=node_build /app/publish .
ENTRYPOINT ["dotnet", "Backend.dll", "--environment=Production"]