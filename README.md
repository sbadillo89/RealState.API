# Proyecto de API - RealState

Este es un proyecto de API para la gestión de propiedades, desarrollado en **.NET 8** utilizando los patrones **CQRS** y **Repository Pattern** para una arquitectura limpia y escalable. Además, se implementó **FluentValidation** para la validación de modelos.

A continuación se detallan los pasos para configurar y ejecutar el proyecto correctamente.

## 🚀 Configuración inicial

### 1. Ejecutar el script de la base de datos

Antes de ejecutar el proyecto, es necesario configurar la base de datos. Para ello, sigue estos pasos:

- **Ubicación del script**: El script SQL necesario para crear las tablas y la estructura de la base de datos se encuentra en la carpeta `scripts/`.
- **Ejecutar el script**: Conéctate a tu servidor de base de datos preferido (por ejemplo, SQL Server Management Studio o Azure Data Studio) y ejecuta el script `setup-database.sql`. Esto creará las tablas y estructuras necesarias en la base de datos, así como la insercion inicial de un `Owner`.

### 2. Configurar la cadena de conexión en `appsettings.json`

Una vez que la base de datos esté configurada, es necesario agregar la cadena de conexión a la base de datos en el archivo `appsettings.json`.

1. Abre el archivo `appsettings.json` en la raíz del proyecto.
2. Busca la sección de **ConnectionStrings** y reemplaza el valor de `PropertiesDB` con la cadena de conexión correspondiente a tu base de datos.

   Ejemplo:

   ```json
   "ConnectionStrings": {
     "PropertiesDB": "Server=localhost;Database=RealStateDb;User Id=myusername;Password=mypassword;"
   }
   ```
