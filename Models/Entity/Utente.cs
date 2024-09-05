using Microsoft.AspNetCore.Identity;

namespace MagazziniMaterialiAPI.Models.Entity
{
    public class Utente : IdentityUser
    {
        public string Nome { get; set; }
        public string Cognome { get; set; }
        public Ruolo Ruolo { get; set; }

    }

    public enum Ruolo
    {
        Admin,
        Utente
    }
}
