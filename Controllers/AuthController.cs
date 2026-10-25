using Microsoft.AspNetCore.Mvc;
using MyPersonalBlog.Dtos;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

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

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
            var credenciales = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var descriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
            new Claim(ClaimTypes.Name, dto.Usuario),
            new Claim(ClaimTypes.Role, "Admin")
        }),
                    Issuer = _configuration["Jwt:Issuer"],
                    Audience = _configuration["Jwt:Audience"],
                    Expires = DateTime.UtcNow.AddHours(2),
                    SigningCredentials = credenciales
                };

            var token = new JsonWebTokenHandler().CreateToken(descriptor);

            return Ok(new { token });


        }
    }
}
