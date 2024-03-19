using Infrastructure.Entities.Generics;
using Infrastructure.Entities.Users;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure
{
    public class LANHQDbContext(DbContextOptions<LANHQDbContext> options) : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>(options)
    {
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

            modelBuilder.Entity<AuditableEntity>(entity =>
            {
                entity
                    .Property(e => e.Id)
                    .HasDefaultValueSql("uuid_generate_v4()");

                entity
                    .Property(e => e.CreatedAt)
                    .HasDefaultValueSql("GETDATE()");

                entity
                    .Property(e => e.UpdatedAt)
                    .HasDefaultValueSql("GETDATE()");
            });
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }
}
