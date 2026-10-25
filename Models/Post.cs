using System.ComponentModel.DataAnnotations;

namespace MyPersonalBlog.Models
{
    public class Post
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El título del post es obligatorio.")]
        [StringLength(100, ErrorMessage = "El título no puede tener más de 100 caracteres.")]
        public string Titulo { get; set; } = string.Empty;

        [Required(ErrorMessage = "El contenido del post no puede estar vacío.")]
        public string Contenido { get; set; } = string.Empty;
        public string Autor { get; set; } = string.Empty;

        public DateTime FechaCreacion { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Todo post debe pertenecer a una categoría.")]
        public int CategoriaId { get; set; }

        public Categoria? Categoria { get; set; }
    }
}