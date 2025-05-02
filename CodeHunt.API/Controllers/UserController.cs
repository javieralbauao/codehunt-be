using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using CodeHunt.API.Data;
using CodeHunt.API.Models;

namespace CodeHunt.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UserController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("getusers")]
        [Authorize]
        public IActionResult GetUsers()
        {
            var users = _context.Users
                .Select(u => new {
                    u.Id,
                    u.Name,
                    u.Email,
                    u.RegisteredAt
                })
                .ToList();

            return Ok(users);
        }
        
        [HttpGet("test")]
        [AllowAnonymous]
        public IActionResult Test()
        {
            return Ok("API funcionando correctamente");
        }
    }
}
