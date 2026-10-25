using System.ComponentModel.DataAnnotations;

namespace MyPersonalBlog.Dtos
{
    public class CrearCategoriaDto
    {
        [Required(ErrorMessage = "Este campo es obligatorio")]
        [StringLength(50, ErrorMessage = "Este campo no puede superar 50 caracteres")]
        public string Nombre { get; set; } = string.Empty;

        [StringLength(200, ErrorMessage = "Este campo no puede superar 200 caracteres")]
        public string Descripcion { get; set; } = string.Empty;
    }
}