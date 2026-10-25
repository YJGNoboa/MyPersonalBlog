using MyPersonalBlog.Models;
using System.ComponentModel.DataAnnotations;

namespace MyPersonalBlog.Dtos
{
    public class CrearPostDto
    {

        [Required(ErrorMessage = "El título del post es obligatorio.")]
        [StringLength(100, ErrorMessage = "El título no puede tener más de 100 caracteres.")]
        public string Titulo { get; set; } = string.Empty;

        [Required(ErrorMessage = "El contenido del post no puede estar vacío.")]
        public string Contenido { get; set; } = string.Empty;

        [Required(ErrorMessage = "El autor es obligatorio.")]
        [StringLength(50, ErrorMessage = "El nombre del autor es muy largo.")]
        public string Autor { get; set; } = string.Empty;


        [Range(1, int.MaxValue, ErrorMessage = "Todo post debe pertenecer a una categoria")]
        public int CategoriaId { get; set; }

    }
}
