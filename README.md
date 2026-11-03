# 📝 MyPersonalBlog API

API REST para un blog personal, desarrollada con **.NET 10**, **ASP.NET Core** y **Entity Framework Core** sobre **PostgreSQL**.
Permite gestionar posts, categorías y comentarios. La lectura es pública y la administración está protegida con **autenticación JWT**.

🌐 **Demo en vivo:** [mypersonalblog-g5jb.onrender.com/api/Post](https://mypersonalblog-g5jb.onrender.com/api/Post)

> ⏳ La API está alojada en el plan gratuito de Render: si no recibe tráfico durante un tiempo, se suspende, y la primera petición puede tardar hasta un minuto en responder.

---

## 🛠️ Tecnologías

- **.NET 10** · C#
- **ASP.NET Core** Web API
- **Entity Framework Core** (Code First + Migraciones)
- **PostgreSQL**, alojado en [Neon](https://neon.com)
- **JWT** (JSON Web Tokens) para autenticación
- **Docker** (build multi-stage)
- **Render** para el despliegue, con deploy automático desde GitHub

---

## 🏗️ Arquitectura

```
┌──────────────┐   HTTPS   ┌─────────────────────────┐   SSL   ┌─────────────────────┐
│   Cliente    │ ────────► │  API (.NET 10)          │ ──────► │  PostgreSQL         │
│ (Postman /   │           │  Contenedor Docker      │         │  Neon (serverless)  │
│  frontend)   │ ◄──────── │  en Render              │ ◄────── │                     │
└──────────────┘           └─────────────────────────┘         └─────────────────────┘
```

La API y la base de datos están **desacopladas**: la API corre en un contenedor sin estado, y los datos viven en un servicio de PostgreSQL administrado.

---

## ✨ Características

- CRUD completo de **posts**, **categorías** y **comentarios**.
- **DTOs** para separar las entidades de la base de datos de los datos que entran y salen de la API.
- **Validación** de datos con Data Annotations.
- **Códigos HTTP** adecuados para cada caso (200, 204, 400, 401, 404, 409).
- **Reglas de integridad:** no se puede eliminar una categoría que tenga posts asociados.
- **Autenticación JWT:** la lectura es pública; crear, editar y eliminar requiere ser administrador.
- **Fechas en UTC**, independientes de la zona horaria del servidor.
- **Configuración segura:** ningún secreto está en el repositorio. Se usan User Secrets en desarrollo y variables de entorno en producción.

---

## 📌 Endpoints

🌐 = público · 🔒 = requiere token

### Autenticación
| Método | Ruta | Acceso | Descripción |
|---|---|---|---|
| POST | `/api/Auth/login` | 🌐 | Inicia sesión y devuelve un token JWT |

### Posts
| Método | Ruta | Acceso | Descripción |
|---|---|---|---|
| GET | `/api/Post` | 🌐 | Lista todos los posts con su categoría |
| GET | `/api/Post/{id}` | 🌐 | Obtiene un post |
| POST | `/api/Post` | 🔒 | Crea un post |
| PUT | `/api/Post/{id}` | 🔒 | Edita un post |
| DELETE | `/api/Post/{id}` | 🔒 | Elimina un post |

### Categorías
| Método | Ruta | Acceso | Descripción |
|---|---|---|---|
| GET | `/api/Categoria` | 🌐 | Lista todas las categorías |
| GET | `/api/Categoria/{id}` | 🌐 | Obtiene una categoría |
| POST | `/api/Categoria` | 🔒 | Crea una categoría |
| PUT | `/api/Categoria/{id}` | 🔒 | Edita una categoría |
| DELETE | `/api/Categoria/{id}` | 🔒 | Elimina una categoría (409 si tiene posts) |

### Comentarios
| Método | Ruta | Acceso | Descripción |
|---|---|---|---|
| GET | `/api/Comentario` | 🌐 | Lista todos los comentarios |
| GET | `/api/Comentario/{id}` | 🌐 | Obtiene un comentario |
| POST | `/api/Comentario` | 🌐 | Crea un comentario |
| PUT | `/api/Comentario/{id}` | 🔒 | Edita un comentario (moderación) |
| DELETE | `/api/Comentario/{id}` | 🔒 | Elimina un comentario (moderación) |

---

## 🔐 Cómo usar la autenticación

1. Haz login:
   ```http
   POST /api/Auth/login
   Content-Type: application/json

   {
     "usuario": "tu-usuario",
     "password": "tu-contraseña"
   }
   ```
2. Copia el `token` de la respuesta.
3. Envíalo en las peticiones protegidas, en el encabezado:
   ```
   Authorization: Bearer <token>
   ```

El token vence a las **2 horas**.

---

## 🚀 Cómo ejecutarlo localmente

### Requisitos
- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- Herramienta de EF Core:
  ```bash
  dotnet tool install --global dotnet-ef
  ```
- Una base de datos **PostgreSQL**. Puede ser una base gratuita en [Neon](https://neon.com), o una local con Docker:
  ```bash
  docker run --name blog-postgres -e POSTGRES_PASSWORD=postgres -p 5432:5432 -d postgres
  ```

### Pasos

**1. Clonar el repositorio**
```bash
git clone https://github.com/YJGNoboa/MyPersonalBlog.git
cd MyPersonalBlog
```

**2. Configurar los secretos con User Secrets**

Ningún secreto se guarda en el repositorio:
```bash
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Host=localhost;Database=blog;Username=postgres;Password=postgres"
dotnet user-secrets set "Jwt:Key" "UNA-CLAVE-ALEATORIA-DE-AL-MENOS-32-CARACTERES"
dotnet user-secrets set "Admin:Usuario" "tu-usuario"
dotnet user-secrets set "Admin:Password" "tu-contraseña"
```

**3. Crear las tablas**
```bash
dotnet ef database update
```

**4. Ejecutar la API**
```bash
dotnet run --launch-profile https
```

La API queda disponible en `https://localhost:7044`.

---

## 🐳 Ejecutar con Docker

**1. Construir la imagen**
```bash
docker build -t mypersonalblog .
```

**2. Crear un archivo `docker.env`** con las variables de entorno. ⚠️ Este archivo está en el `.gitignore`: nunca debe subirse al repositorio.
```
ConnectionStrings__DefaultConnection=Host=...;Database=...;Username=...;Password=...;SSL Mode=Require
Jwt__Key=UNA-CLAVE-ALEATORIA-DE-AL-MENOS-32-CARACTERES
Admin__Usuario=tu-usuario
Admin__Password=tu-contraseña
```

**3. Ejecutar el contenedor**
```bash
docker run --rm -p 8080:8080 --env-file docker.env mypersonalblog
```

La API queda disponible en `http://localhost:8080`.

---

## 📁 Estructura del proyecto

```
MyPersonalBlog/
├── Controllers/     # Endpoints de la API
├── Data/            # ApplicationDbContext (EF Core)
├── Dtos/            # Objetos de entrada y salida de la API
├── Migrations/      # Migraciones de la base de datos
├── Models/          # Entidades: Post, Categoria, Comentario
├── Dockerfile       # Imagen de producción (multi-stage)
└── Program.cs       # Configuración de servicios, JWT y pipeline
```

---

## 🗺️ Próximos pasos

- [x] DTOs y validaciones
- [x] Autenticación con JWT
- [x] Migración a PostgreSQL
- [x] Contenerización con Docker
- [x] Despliegue en la nube (Render + Neon)
- [ ] Documentación interactiva con Scalar
- [ ] Frontend con Razor Pages
- [ ] Pruebas automatizadas

---

## 👤 Autor

**Yanel González** · Estudiante de Desarrollo de Software en el ITLA

[GitHub](https://github.com/YJGNoboa) · [LinkedIn](https://www.linkedin.com/in/TU-USUARIO)
