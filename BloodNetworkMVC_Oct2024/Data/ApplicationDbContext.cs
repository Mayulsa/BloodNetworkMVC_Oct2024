using BloodNetworkMVC_Oct2024.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BloodNetworkMVC_Oct2024.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<BloodUnit> BloodUnits { get; set; }
        public DbSet<Freezer> Freezers { get; set; }
        public DbSet<Drawer> Drawers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure BloodUnit entity
            modelBuilder.Entity<BloodUnit>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.BloodGroup)
                    .IsRequired()
                    .HasMaxLength(5);

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(20);

                // Relationship with Freezer
                entity.HasOne(b => b.Freezer)
                    .WithMany(f => f.BloodUnits)
                    .HasForeignKey(b => b.FreezerId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired();

                // Relationship with Drawer
                entity.HasOne(b => b.Drawer)
                    .WithMany(d => d.BloodUnits)
                    .HasForeignKey(b => b.DrawerId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired();
            });

            // Configure Freezer entity
            modelBuilder.Entity<Freezer>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Location)
                    .IsRequired()
                    .HasMaxLength(100);

                // One Freezer can have many Drawers
                entity.HasMany(f => f.Drawers)
                    .WithOne(d => d.Freezer)
                    .HasForeignKey(d => d.FreezerId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired();
            });

            // Configure Drawer entity
            modelBuilder.Entity<Drawer>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Capacity)
                    .IsRequired();

                // Relationship with BloodUnits is already defined above
            });

            // Add indexes for better query performance
            modelBuilder.Entity<BloodUnit>()
                .HasIndex(b => b.BloodGroup);

            modelBuilder.Entity<BloodUnit>()
                .HasIndex(b => b.ExpirationDate);

            modelBuilder.Entity<BloodUnit>()
                .HasIndex(b => new { b.FreezerId, b.DrawerId });
        }
    }
}
