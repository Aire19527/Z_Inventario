# Usar la imagen oficial de .NET 6 SDK para construir la aplicación
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

# Establecer el directorio de trabajo en /src
WORKDIR /src

# Copiar los archivos de la solución y restaurar las dependencias
COPY MVCWEB.sln ./
COPY Web/Web.csproj Web/
COPY Domain/Domain.csproj Domain/
COPY Infraestructure.Core/Infraestructure.Core.csproj Infraestructure.Core/
COPY Infraestructure.Entity/Infraestructure.Entity.csproj Infraestructure.Entity/
COPY Common.Utils/Common.Utils.csproj Common.Utils/
RUN dotnet restore

# Copiar el resto de los archivos y construir la aplicación
COPY . .
WORKDIR /src/Web
RUN dotnet publish -c Release -o /app/out

# Usar la imagen oficial de ASP.NET Core Runtime para ejecutar la aplicación
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime

# Establecer el directorio de trabajo en /app
WORKDIR /app

# Copiar los archivos compilados desde la fase de construcción
COPY --from=build /app/out .

# Exponer el puerto 80 para el tráfico HTTP
EXPOSE 80

# Definir el comando para ejecutar la aplicación
ENTRYPOINT ["dotnet", "Web.dll"]
