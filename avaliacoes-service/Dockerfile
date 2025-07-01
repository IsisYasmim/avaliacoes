# Imagem base para tempo de execução
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
ENV ASPNETCORE_URLS=http://+:80

# Imagem para build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar csproj e restaurar dependências
COPY ["avaliacoes-service.csproj", "."]
RUN dotnet restore "avaliacoes-service.csproj"

# Copiar todo o código
COPY . .

# Build do projeto
RUN dotnet build "avaliacoes-service.csproj" -c Release -o /app/build

# Publicar aplicação
FROM build AS publish
RUN dotnet publish "avaliacoes-service.csproj" -c Release -o /app/publish

# Imagem final
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "avaliacoes-service.dll"]