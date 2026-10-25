using MyPersonalBlog.Models;

namespace MyPersonalBlog.Dtos
{
    public class CategoriaDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;

        public static CategoriaDto DesdeEntidad(Categoria categoria) 
        {
            return new CategoriaDto
            {
                Id = categoria.Id,
                Nombre = categoria.Nombre,
                Descripcion = categoria.Descripcion,
            };
        }
    }
}
