using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyPersonalBlog.Data;
using MyPersonalBlog.Models;
using MyPersonalBlog.Dtos;

namespace MyPersonalBlog.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class PostController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PostController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var post = await _context.Posts
                .Include(p => p.Categoria)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (post == null) { return NotFound(); }

            return Ok(PostDto.DesdeEntidad(post));
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var posts = await _context.Posts.Include(p => p.Categoria).ToListAsync();
            return Ok(posts.Select(p => PostDto.DesdeEntidad(p)));
        }        

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var post = await _context.Posts.FindAsync(id);

            if (post == null) { return NotFound(); }

            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpPost]
        public async Task<ActionResult> Create(CrearPostDto dto)
        {
            var categoria = await _context.Categories.FindAsync(dto.CategoriaId);

            if (categoria == null)
            {
                return BadRequest("La categoría indicada no existe.");
            }
           
            var newPost = new Post
            {
                Titulo = dto.Titulo,
                Contenido = dto.Contenido,
                Autor = dto.Autor,
                CategoriaId = dto.CategoriaId,
            };

            await _context.Posts.AddAsync(newPost);
            await _context.SaveChangesAsync();

            return Ok(PostDto.DesdeEntidad(newPost));
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(CrearPostDto dto, int id)
        {
            var postValidation = await _context.Posts.FindAsync(id);
            
            if(postValidation == null) { return NotFound(); }

            var categoriaExiste = await _context.Categories.AnyAsync(c => c.Id == dto.CategoriaId);

            if(!categoriaExiste) { return BadRequest("La categoria indicada no existe"); }


            postValidation.Titulo = dto.Titulo;
            postValidation.Contenido = dto.Contenido;
            postValidation.Autor = dto.Autor;
            postValidation.CategoriaId = dto.CategoriaId;
        

            await _context.SaveChangesAsync();

            return Ok(PostDto.DesdeEntidad(postValidation));
        }


    }
}
