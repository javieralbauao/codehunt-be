using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using CodeHunt.API.DTOs;
using CodeHunt.API.Services;
using CodeHunt.API.Models;

namespace CodeHunt.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _auth;

        public AuthController(AuthService auth)
        {
            _auth = auth;
        }

        // POST: api/auth/register
        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Email) || string.IsNullOrWhiteSpace(dto.Password))
                return BadRequest("Correo y contraseña son obligatorios.");

            if (dto.Password.Length < 6)
                return BadRequest("La contraseña debe tener al menos 6 caracteres.");

            var user = new User
            {
                Name = dto.Name,
                Email = dto.Email,
                PasswordHash = dto.Password,
                RoleId = Guid.Parse("0AEB0E60-4B9F-4ED6-9460-DD4F79C7BD3E") 
            };

            var result = _auth.Register(user);
            if (!result) return Conflict("El correo ya está registrado.");

            return Ok("Registro exitoso.");
        }

        // POST: api/auth/login
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDTO dto)
        {
            var user = _auth.ValidateUser(dto.Email, dto.Password);
            if (user == null)
                return Unauthorized("Credenciales inválidas");

            var token = _auth.GenerateToken(user);
            return Ok(new { token });
        }

        // GET: api/auth/test
        [HttpGet("test")]
        [Authorize]
        public IActionResult TestJWT()
        {
            var username = User.Identity?.Name;
            return Ok($"Hola {username}, estás autenticado con JWT");
        }
    }
}
