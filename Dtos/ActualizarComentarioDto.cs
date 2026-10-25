using System.ComponentModel.DataAnnotations;

namespace MyPersonalBlog.Dtos
{
    public class ActualizarComentarioDto
    {
        [Required(ErrorMessage = "El autor es obligatorio")]
        [StringLength(50, ErrorMessage = "Este campo no puede superar 50 caracteres")]
        public string Autor { get; set; } = string.Empty;

        [Required(ErrorMessage = "El contenido del comentario es obligatorio.")]
        [StringLength(500, ErrorMessage = "El comentario no puede exceder los 500 caracteres.")]
        public string Contenido { get; set; } = string.Empty;

    }
}
