using System.ComponentModel.DataAnnotations;

namespace MyPersonalBlog.Dtos
{
    public class CrearComentarioDto
    {
        [Required(ErrorMessage = "El autor es obligatorio.")]
        [StringLength(50, ErrorMessage = "El nombre del autor es muy largo.")]
        public string Autor { get; set; } = string.Empty;

        [Required(ErrorMessage = "El contenido del comentario es obligatorio.")]
        [StringLength(500, ErrorMessage = "El comentario no puede exceder los 500 caracteres.")]
        public string Contenido { get; set; } = string.Empty;

        [Range(1, int.MaxValue, ErrorMessage = "Todo comentario debe pertenecer a un post")]
        public int PostId { get; set; }

    }
}
