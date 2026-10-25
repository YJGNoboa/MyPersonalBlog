# 📝 MyPersonalBlog API

API REST para un blog personal, desarrollada con **.NET 10**, **ASP.NET Core** y **Entity Framework Core** sobre **SQLite**.
Permite gestionar posts, categorías y comentarios. La lectura es pública y la administración está protegida con **autenticación JWT**.

> Proyecto personal de aprendizaje y portafolio. 🚧 En desarrollo.

---

## 🛠️ Tecnologías

- .NET 10 · C#
- ASP.NET Core Web API
- Entity Framework Core (Code First + Migraciones)
- SQLite
- JWT (JSON Web Tokens)

---

## ✨ Características

- CRUD completo de **posts**, **categorías** y **comentarios**.
- **DTOs** para separar las entidades de la base de datos de los datos que entran y salen de la API.
- **Validación** de datos con Data Annotations.
- **Códigos HTTP** adecuados para cada caso (200, 204, 400, 401, 404, 409).
- **Reglas de integridad:** no se puede eliminar una categoría que tenga posts asociados.
- **Autenticación JWT:** la lectura es pública; crear, editar y eliminar requiere ser administrador.
- **Secretos fuera del repositorio** mediante User Secrets.

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
| PUT | `/api/Comentario/{id}` | 🔒 | Edita un comentario |
| DELETE | `/api/Comentario/{id}` | 🔒 | Elimina un comentario |

---

## 🚀 Cómo ejecutarlo localmente

### Requisitos
- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- Herramienta de EF Core:
  ```bash
  dotnet tool install --global dotnet-ef
  ```

### Pasos

**1. Clonar el repositorio**
```bash
git clone https://github.com/YJGNoboa/MyPersonalBlog.git
cd MyPersonalBlog
```

**2. Configurar los secretos**

La clave JWT y las credenciales del administrador no se guardan en el repositorio. Configúralas con User Secrets:
```bash
dotnet user-secrets set "Jwt:Key" "UNA-CLAVE-ALEATORIA-DE-AL-MENOS-32-CARACTERES"
dotnet user-secrets set "Admin:Usuario" "tu-usuario"
dotnet user-secrets set "Admin:Password" "tu-contraseña"
```

**3. Crear la base de datos**
```bash
dotnet ef database update
```

**4. Ejecutar la API**
```bash
dotnet run --launch-profile https
```

La API queda disponible en `https://localhost:7044`.

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

## 📁 Estructura del proyecto

```
MyPersonalBlog/
├── Controllers/   # Endpoints de la API
├── Data/          # ApplicationDbContext (EF Core)
├── Dtos/          # Objetos de entrada y salida de la API
├── Migrations/    # Migraciones de la base de datos
├── Models/        # Entidades: Post, Categoria, Comentario
└── Program.cs     # Configuración de servicios, JWT y pipeline
```

---

## 🗺️ Próximos pasos

- [x] DTOs y validaciones
- [x] Autenticación con JWT
- [ ] Documentación interactiva con Scalar
- [ ] Despliegue en la nube (Render / Railway)
- [ ] Frontend con Razor Pages

---

## 👤 Autor

**Yanel González** · Estudiante de Desarrollo de Software en el ITLA

[GitHub](https://github.com/YJGNoboa) · [LinkedIn](https://www.linkedin.com/in/TU-USUARIO)
