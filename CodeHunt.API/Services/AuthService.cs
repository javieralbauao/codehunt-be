using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using BCrypt.Net;
using CodeHunt.API.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using CodeHunt.API.Data;
using Microsoft.EntityFrameworkCore;

namespace CodeHunt.API.Services
{
    public class AuthService
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _config;
        public AuthService(AppDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        public bool Register(User user)
        {
            if (_context.Users.Any(u => u.Email == user.Email))
                return false;

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash);
            _context.Users.Add(user);
            _context.SaveChanges();
            return true;
        }

        public User? ValidateUser(string email, string password)
        {
            var user = _context.Users
                .Include(u => u.Role)
                .FirstOrDefault(u => u.Email == email);

            if (user is null) return null;

            return BCrypt.Net.BCrypt.Verify(password, user.PasswordHash) ? user : null;
        }

        public string GenerateToken(User user)
        {
            var jwtKey = _config["Jwt:Key"] ?? "";
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Role.Name)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "codehunt.api",
                audience: "codehunt.client",
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
