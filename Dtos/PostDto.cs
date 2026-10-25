using MyPersonalBlog.Models;

namespace MyPersonalBlog.Dtos
{
    public class PostDto
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Contenido { get; set; } = string.Empty;
        public string Autor { get; set; } = string.Empty;
        public DateTime FechaCreacion { get; set; }
        public int CategoriaId { get; set; }
        public string CategoriaNombre { get; set; } = string.Empty;

        public static PostDto DesdeEntidad(Post post)
        {
            return new PostDto
            {
                Id = post.Id,
                Titulo = post.Titulo,
                Contenido = post.Contenido,
                Autor = post.Autor,
                FechaCreacion = post.FechaCreacion,
                CategoriaId = post.CategoriaId,
                CategoriaNombre = post.Categoria?.Nombre ?? string.Empty
            };
        }
    }
}