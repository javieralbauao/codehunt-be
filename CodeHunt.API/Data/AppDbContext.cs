using Microsoft.EntityFrameworkCore;
using CodeHunt.API.Models;

namespace CodeHunt.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Submission> Submissions { get; set; }
        public DbSet<TestCase> TestCases { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<CodeTemplate> CodeTemplates { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Property(u => u.RegisteredAt)
                .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<Exercise>()
                .Property(e => e.CreatedAt)
                .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<Submission>()
                .Property(s => s.SubmittedAt)
                .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<TestCase>()
                .Property(t => t.IsPublic)
                .HasDefaultValue(true);
        }
    }
}