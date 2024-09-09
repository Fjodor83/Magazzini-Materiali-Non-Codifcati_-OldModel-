using MagazziniMaterialiAPI.Models.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace MagazziniMaterialiAPI.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Magazzino> Magazzini { get; set; }
        public DbSet<Materiale> Materiali { get; set; }
        public DbSet<Classificazione> Classificazioni { get; set; }
        public DbSet<MaterialeImmagine> MaterialeImmagini { get; set; }

        // Nuove entità
        public DbSet<Movimentazione> Movimentazioni { get; set; }
        public DbSet<Giacenza> Giacenze { get; set; }
        public DbSet<MissionePrelievo> MissioniPrelievo { get; set; }
        public DbSet<DettaglioMissione> DettagliMissione { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Configurazioni aggiuntive per le nuove entità
            builder.Entity<Movimentazione>()
                .HasOne(m => m.Materiale)
                .WithMany()
                .HasForeignKey(m => m.MaterialeId);

            builder.Entity<Movimentazione>()
                .HasOne(m => m.Magazzino)
                .WithMany()
                .HasForeignKey(m => m.MagazzinoId);

            builder.Entity<Giacenza>()
                .HasOne(g => g.Materiale)
                .WithMany()
                .HasForeignKey(g => g.MaterialeId);

            builder.Entity<Giacenza>()
                .HasOne(g => g.Magazzino)
                .WithMany()
                .HasForeignKey(g => g.MagazzinoId);

            builder.Entity<MissionePrelievo>()
                .HasMany(m => m.DettagliMissione)
                .WithOne(d => d.MissionePrelievo)
                .HasForeignKey(d => d.MissionePrelievoId);

            builder.Entity<DettaglioMissione>()
                .HasOne(d => d.Materiale)
                .WithMany()
                .HasForeignKey(d => d.MaterialeId);

            builder.Entity<MissionePrelievo>()
                .HasOne(m => m.Operatore)
                .WithMany()
                .HasForeignKey(m => m.OperatoreId);
            builder.Entity<Materiale>()
                .HasMany(m => m.Immagini)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}