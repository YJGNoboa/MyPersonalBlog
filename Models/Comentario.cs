using System.ComponentModel.DataAnnotations;

namespace MyPersonalBlog.Models
{
    public class Comentario
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El autor es obligatorio.")]
        [StringLength(50, ErrorMessage = "El nombre del autor es muy largo.")]
        public string Autor { get; set; } = string.Empty;

        [Required(ErrorMessage = "El contenido del comentario es obligatorio.")]
        [StringLength(500, ErrorMessage = "El comentario no puede exceder los 500 caracteres.")]
        public string Contenido { get; set; } = string.Empty;

        public DateTime FechaCreacion { get; set; } = DateTime.Now;

        [Required]
        public int PostId { get; set; }

        public Post? Post { get; set; }
    }
}