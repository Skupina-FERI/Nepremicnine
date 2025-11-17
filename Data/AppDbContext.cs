using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RZ_nepremicnine.Models;

namespace RZ_nepremicnine.Data
{
    public class AppDbContext : IdentityDbContext<Uporabniki>
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Nepremicnina> Nepremicnine { get; set; }
        public DbSet<PropertyImage> PropertyImages { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Configure relationships
            builder.Entity<Nepremicnina>()
                .HasOne(n => n.Owner)
                .WithMany(u => u.OwnedProperties)
                .HasForeignKey(n => n.UporabnikiId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.Entity<PropertyImage>()
                .HasOne(pi => pi.Nepremicnina)
                .WithMany(n => n.Images)
                .HasForeignKey(pi => pi.NepremicninaId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}