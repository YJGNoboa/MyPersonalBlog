using Microsoft.AspNetCore.Mvc;
using MyPersonalBlog.Dtos;

namespace MyPersonalBlog.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost("login")]
        public ActionResult Login(LoginDto dto)
        {
            var usuarioAdmin = _configuration["Admin:Usuario"];
            var passwordAdmin = _configuration["Admin:Password"];

            if(dto.Usuario != usuarioAdmin || dto.Password != passwordAdmin)
            {
                return Unauthorized("Usuario o contraseña incorrecta");
            }

            return Ok("Credenciales correctas");
        }
    }
}
