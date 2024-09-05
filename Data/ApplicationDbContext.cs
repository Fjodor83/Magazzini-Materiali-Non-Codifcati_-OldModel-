using MagazziniMaterialiAPI.Models.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MagazziniMaterialiAPI.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {
        }

        public DbSet<Magazzino> Magazzini { get; set; }
        public DbSet<Materiale> Materiali { get; set; }
        public DbSet<Classificazione> Classificazioni { get; set; }
        public DbSet<MaterialeImmagine> MaterialeImmagini { get; set; }
        public DbSet<Utente> Utenti { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Qui puoi aggiungere configurazioni aggiuntive se necessario
        }
    } 

}
