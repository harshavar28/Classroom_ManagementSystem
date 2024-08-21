using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using cmsdn.Models;
using Microsoft.Extensions.Options;

namespace cmsdn.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Admin> Admins { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Password> Passwords { get; set; }
        public DbSet<Attendance> Attendance { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed an initial admin user
            modelBuilder.Entity<Admin>().HasData(new Admin
            {
                Id = 1,
                Email = "admin@example.com",
                Password = "admin123" // Store hashed passwords in production!
            });

            // Seed an initial common password
            modelBuilder.Entity<Password>().HasData(new Password
            {
                Id = 1,
                CommonPassword = "common123"
            });
        }
    }

}
