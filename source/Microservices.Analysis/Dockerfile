FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG ARG_ENVIRONMENT
ENV ENVIRONMENT $ARG_ENVIRONMENT
WORKDIR /src
COPY ["nuget.config", "."]
COPY ["source/Microservices.Analysis/Microservices.Analysis.csproj", "source/Microservices.Analysis/"]
RUN dotnet restore "source/Microservices.Analysis/Microservices.Analysis.csproj"
COPY . .
WORKDIR "/src/source/Microservices.Analysis"
RUN dotnet build "Microservices.Analysis.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Microservices.Analysis.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Microservices.Analysis.dll"]