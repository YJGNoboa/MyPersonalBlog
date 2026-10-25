using MyPersonalBlog.Models;

namespace MyPersonalBlog.Dtos
{
    public class ComentarioDto
    {
        public int Id { get; set; }
        public string Autor { get; set; } = string.Empty;
        public string Contenido { get; set; } = string.Empty;
        public DateTime FechaCreacion { get; set; }
        public int PostId { get; set; }

        public static ComentarioDto DesdeEntidad(Comentario comentario)
        {
            return new ComentarioDto
            {
                Id = comentario.Id,
                Autor = comentario.Autor,
                Contenido = comentario.Contenido,
                FechaCreacion = comentario.FechaCreacion,
                PostId = comentario.PostId,
            };
        }
    }
}