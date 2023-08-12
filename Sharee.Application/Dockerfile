FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Sharee.Application/Sharee.Application.csproj", "Sharee.Application/"]
RUN dotnet restore "Sharee.Application/Sharee.Application.csproj"
COPY . .
WORKDIR "/src/Sharee.Application"
RUN dotnet build "Sharee.Application.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Sharee.Application.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Sharee.Application.dll"]
