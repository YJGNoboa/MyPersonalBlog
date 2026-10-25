using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyPersonalBlog.Data;
using MyPersonalBlog.Dtos;
using MyPersonalBlog.Models;

namespace MyPersonalBlog.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class ComentarioController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ComentarioController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var comentario = await _context.Comentarios.FindAsync(id);

            if (comentario == null) { return NotFound(); }

            return Ok(ComentarioDto.DesdeEntidad(comentario));
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var comentario = await _context.Comentarios
                .Select(c => ComentarioDto.DesdeEntidad(c))
                .ToListAsync();

            return Ok(comentario);
        }
        [HttpPost]
        public async Task<ActionResult> Create(CrearComentarioDto dto)
        {
            var postExiste = await _context.Posts.AnyAsync(p => p.Id == dto.PostId);

            if(!postExiste) { return BadRequest("El post indicado no existe"); }

            var newComentario = new Comentario
            {
                Autor = dto.Autor,
                Contenido = dto.Contenido,
                PostId = dto.PostId
            };

            await _context.Comentarios.AddAsync(newComentario);
            await _context.SaveChangesAsync();
                       
            return Ok(ComentarioDto.DesdeEntidad(newComentario));
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var comentarios = await _context.Comentarios.FindAsync(id);

            if (comentarios == null) { return NotFound();  }

            _context.Comentarios.Remove(comentarios);

            await _context.SaveChangesAsync();

            return Ok(comentarios);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, ActualizarComentarioDto dto)
        {
            var updateComentario = await _context.Comentarios.FindAsync(id);

            if (updateComentario == null) { return NotFound(); }

            updateComentario.Autor = dto.Autor;
            updateComentario.Contenido = dto.Contenido;
            
            await _context.SaveChangesAsync();

            return Ok(ComentarioDto.DesdeEntidad(updateComentario));
        }
    }
}
