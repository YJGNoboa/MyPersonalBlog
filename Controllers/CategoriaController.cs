using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyPersonalBlog.Data;
using MyPersonalBlog.Dtos;
using MyPersonalBlog.Models;

namespace MyPersonalBlog.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    [Authorize]
    public class CategoriaController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CategoriaController(ApplicationDbContext context)
        {
            _context = context;
        }
        
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult> GetById(int id)
        {
            var categoria = await _context.Categories.FindAsync(id);

            if (categoria == null) { return NotFound(); }

            return Ok(CategoriaDto.DesdeEntidad(categoria));
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> GetAll()
        {
            var categorias = await _context.Categories
                .Select(c => CategoriaDto.DesdeEntidad(c))
                .ToListAsync();

            return Ok(categorias);
        }
        [HttpPost]
        public async Task<ActionResult> Create(CrearCategoriaDto dto)
        {
            var newCategoria = new Categoria
            {
               Nombre = dto.Nombre,
               Descripcion = dto.Descripcion,
            };

            await _context.Categories.AddAsync(newCategoria);
            await _context.SaveChangesAsync();

            return Ok(CategoriaDto.DesdeEntidad(newCategoria));
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var categoria = await _context.Categories.FindAsync(id);

            if(categoria == null) { return NotFound(); }

            var tienePost = await _context.Posts.AnyAsync(p => p.CategoriaId == categoria.Id);

            if(tienePost)
            {
                return Conflict("No puedes eliminar una categoria que tiene post");
            }

            _context.Categories.Remove(categoria);
            await _context.SaveChangesAsync();

            return Ok(categoria);        

        }
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, CrearCategoriaDto dto)
        {
            var categoria = await _context.Categories.FindAsync(id);

            if(categoria == null) { return NotFound(); }

            categoria.Nombre = dto.Nombre;
            categoria.Descripcion = dto.Descripcion;

            await _context.SaveChangesAsync();

            return Ok(CategoriaDto.DesdeEntidad(categoria));

        }
    }
}
