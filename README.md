# Z-Inventario
Proyecto de Integración Continua - Sistema de Inventarios

Este proyecto consiste en un sistema de inventario desarrollado en .NET 6 utilizando C# y sigue una arquitectura MVC con una implementación de microservicios. El sistema gestiona información relacionada con facturas, productos, inventarios, clientes y cálculos de valores de productos con descuentos.

#Arquitectura

El proyecto está diseñado siguiendo una arquitectura de microservicios para lograr una mayor modularidad y escalabilidad. Cada microservicio se encarga de una parte específica del sistema, lo que facilita el mantenimiento y la evolución del mismo.

Tecnologías Utilizadas
Lenguaje de Programación: C# (.NET 6)
Base de Datos: SQL Server

#Funcionalidades Principales

El sistema ofrece las siguientes funcionalidades principales:

Gestión de Facturas: Permite la creación, consulta y modificación de facturas, así como la generación de facturas con detalle de compra.
Gestión de Productos e Inventario: Actualiza automáticamente los inventarios según las facturaciones realizadas.
Gestión de Clientes: Almacena y gestiona la información de los clientes.
Cálculo de Valores con Descuentos: Realiza cálculos automáticos de valores de productos considerando los descuentos aplicados.

#Instalación y Configuración

Para ejecutar el proyecto localmente, sigue estos pasos:

Clona el repositorio en tu máquina local.
Abre la solución en Visual Studio o tu IDE preferido.
Configura la conexión a la base de datos SQL Server en el archivo de configuración correspondiente.
Compila la solución y ejecuta el proyecto.

#Contribuciones

¡Las contribuciones son bienvenidas! Si deseas contribuir al proyecto, por favor sigue estos pasos:

#Crea un fork del repositorio.
Realiza tus cambios en una nueva rama.
Envía un pull request con una descripción clara de tus cambios.
Contacto
Para cualquier pregunta o sugerencia, no dudes en ponerte en contacto con nosotros a través de correo electrónico.

#Pasos Comandos Docker Windows 

##1)Iniciar contenedor e imagen sql server
**docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=P@ssW0rd2024" -p 1433:1433 --name sqlserver -d mcr.microsoft.com/mssql/server:2022-latest**
>El parametro **SA_PASSWORD** configura la constraseña para el usuario **SA** de la instancia de bases de datos sql server
>**--name** es el parametro correspondiente al nombre del contenedor del motor sql server

##2)En el proyecto **3.Infraestructure/Infraestructure.Core** ejecutar la actualizacion de la base de datos dentro de la consola de administrador de paquetes de visual studio con el siguiente comando:
**Update-Database**

##3)En el directorio raiz de la solucion  crear dos archivos **DockerFile** y **.dockerignore** luego ejecutar el siguiente comando para crear la imagen de docker:
**docker build -t z-inventario:latest .**

##4)Para iniciar la aplicacion vinculada al contenedor sql server se ejecuta la siguiente linea de comando:
**docker run -d -p 8080:80 --name z-inventariocontainer --link sqlserver z-inventario:latest**
>**-p** es el parametro asociado al puerto en el que va a ejecutarse la aplicacion del contenedor
>**--name** es el paremtro asociado al nombre del contenedor de la aplicacion
>**--link** es el parametro correspondiente al **--name** del contenedor sql server iniciado anteriormente
>Importante que el **--name** del contenedor del servidor sql server este configurado como Servidor en la conexion de bases de datos de la imagen docker creada de la aplicacion
**"ConnectionStringSQLServer": "Server=sqlserver;Database=BdStore;User Id=sa;Password=P@ssW0rd2024;"**
