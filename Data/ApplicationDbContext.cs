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
        public DbSet<MaterialeMagazzino> MaterialeMagazzini { get; set; }
        public DbSet<Movimentazione> Movimentazioni { get; set; }
        public DbSet<Giacenza> Giacenze { get; set; }
        public DbSet<MissionePrelievo> MissioniPrelievo { get; set; }
        public DbSet<DettaglioMissione> DettagliMissione { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Configura CodiceMateriale come chiave alternativa per la tabella Materiale
            builder.Entity<Materiale>()
                .HasAlternateKey(m => m.CodiceMateriale);  // Chiave alternativa su CodiceMateriale

            // Configurazioni per Movimentazione
            builder.Entity<Movimentazione>()
                .HasOne(m => m.Materiale)
                .WithMany()
                .HasForeignKey(m => m.CodiceMateriale)
                .HasPrincipalKey(m => m.CodiceMateriale);  // Configura relazione tramite CodiceMateriale

            builder.Entity<Movimentazione>()
                .HasOne(m => m.Magazzino)
                .WithMany()
                .HasForeignKey(m => m.MagazzinoId);

            // Configurazioni per Giacenza
            builder.Entity<Giacenza>()
                .HasOne(g => g.Materiale)
                .WithMany()
                .HasForeignKey(g => g.CodiceMateriale)  // Usa CodiceMateriale come chiave esterna
                .HasPrincipalKey(m => m.CodiceMateriale);  // Relazione tramite CodiceMateriale

            builder.Entity<Giacenza>()
                .HasOne(g => g.Magazzino)
                .WithMany()
                .HasForeignKey(g => g.MagazzinoId);

            // Configurazioni per MissionePrelievo e DettaglioMissione
            builder.Entity<MissionePrelievo>()
                .HasMany(m => m.DettagliMissione)
                .WithOne(d => d.MissionePrelievo)
                .HasForeignKey(d => d.MissionePrelievoId);

            builder.Entity<DettaglioMissione>()
                .HasOne(d => d.Materiale)
                .WithMany()
                .HasForeignKey(d => d.CodiceMateriale)  // Usa CodiceMateriale
                .HasPrincipalKey(m => m.CodiceMateriale);  // Relazione tramite CodiceMateriale

            builder.Entity<MissionePrelievo>()
                .HasOne(m => m.Operatore)
                .WithMany()
                .HasForeignKey(m => m.OperatoreId);

            // Configurazioni per Materiale e le sue Immagini
            builder.Entity<Materiale>()
                .HasMany(m => m.Immagini)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);  // Cancella le immagini associate quando un Materiale viene eliminato
        }

    }
}