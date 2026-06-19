# ==========================================
# Estágio 1: Build e Publicação da Aplicação
# ==========================================
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia o arquivo da Solução
COPY *.sln ./

# Copia os arquivos de projeto (.csproj) de cada camada para restaurar as dependências
# (Isso otimiza o cache do Docker para builds mais rápidos)
COPY ENGER.API/*.csproj ./ENGER.API/
COPY ENGER.Application/*.csproj ./ENGER.Application/
COPY ENGER.Domain/*.csproj ./ENGER.Domain/
COPY ENGER.Infrastructure/*.csproj ./ENGER.Infrastructure/

# Executa o restore das dependências de todo o projeto
RUN dotnet restore

# Copia o restante de todos os códigos do projeto para dentro do container
COPY . .

# Altera o diretório para a pasta da API principal e gera os arquivos de produção publicados
WORKDIR "/src/ENGER.API"
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

# ==========================================
# Estágio 2: Ambiente de Execução (Runtime)
# ==========================================
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Copia os arquivos compilados do estágio de build para o estágio final
COPY --from=build /app/publish .

# Define a porta padrão que o Render utiliza para Web Services Docker
ENV ASPNETCORE_URLS=http://+:10000
EXPOSE 10000

# Comando que inicia a aplicação
ENTRYPOINT ["dotnet", "ENGER.API.dll"]