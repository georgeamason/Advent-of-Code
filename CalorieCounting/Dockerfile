FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["CalorieCounting/CalorieCounting.csproj", "CalorieCounting/"]
RUN dotnet restore "CalorieCounting/CalorieCounting.csproj"
COPY . .
WORKDIR "/src/CalorieCounting"
RUN dotnet build "CalorieCounting.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CalorieCounting.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CalorieCounting.dll"]
