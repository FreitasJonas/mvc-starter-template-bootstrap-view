# -------------------------------------------------------
# STAGE 1 - Build
# -------------------------------------------------------
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia tudo da solution
COPY . .

# Restaura dependências da solution inteira
RUN dotnet restore mvc.starter.template.Web/mvc.starter.template.Web.csproj

# Publica o projeto Web
RUN dotnet publish mvc.starter.template.Web/mvc.starter.template.Web.csproj \
    -c Release \
    -o /app/publish \
    /p:UseAppHost=false

# -------------------------------------------------------
# STAGE 2 - Runtime
# -------------------------------------------------------
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "mvc.starter.template.Web.dll"]
