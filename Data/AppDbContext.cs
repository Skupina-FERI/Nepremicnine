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
        public DbSet<Regija> Regije { get; set; }
        public DbSet<Posredovanje> Posredovanja { get; set; }
        public DbSet<VrstaNepremicnine> VrsteNepremicnin { get; set; }


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

            builder.Entity<Regija>().HasData(
            new Regija { Id = 1, Name = "Pomurska" },
            new Regija { Id = 2, Name = "Podravska" },
            new Regija { Id = 3, Name = "Koroška" },
            new Regija { Id = 4, Name = "Savinjska" }
            );

            builder.Entity<Posredovanje>().HasData(
            new Posredovanje { Id = 1, Name = "Prodaja" },
            new Posredovanje { Id = 2, Name = "Oddaja" },
            new Posredovanje { Id = 3, Name = "Nakup" },
            new Posredovanje { Id = 4, Name = "Najem" }
            );

            builder.Entity<VrstaNepremicnine>().HasData(
            new VrstaNepremicnine { Id = 1, Name = "Stanovanje" },
            new VrstaNepremicnine { Id = 2, Name = "Hiša" },
            new VrstaNepremicnine { Id = 3, Name = "Parcela" },
            new VrstaNepremicnine { Id = 4, Name = "Poslovni prostor" }
    );
        }
    }
}