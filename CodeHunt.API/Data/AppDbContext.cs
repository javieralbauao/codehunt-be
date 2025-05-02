using Microsoft.EntityFrameworkCore;
using CodeHunt.API.Models;

namespace CodeHunt.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<User> Users => Set<User>();
    }
}