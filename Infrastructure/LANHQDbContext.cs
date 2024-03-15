using Infrastructure.Entities.Users;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;


namespace Infrastructure
{
    public class LANHQDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public LANHQDbContext(DbContextOptions<LANHQDbContext> options) :
            base(options)
        { }

        public DbSet<Permission> Permissions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Permission>()
               .Property(p => p.Id)
               .ValueGeneratedOnAdd();
            modelBuilder.Entity<Permission>()
                .HasIndex(p => new { p.System, p.Action })
                .IsUnique();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }
}
