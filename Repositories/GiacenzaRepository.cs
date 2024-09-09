using MagazziniMaterialiAPI.Data;
using MagazziniMaterialiAPI.Models.Entity;

namespace MagazziniMaterialiAPI.Repositories
{
    public class GiacenzaRepository : IGiacenzaRepository
    {
        private readonly ApplicationDbContext _context;

        public GiacenzaRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Giacenza GetGiacenza(int magazzinoId, int materialeId)
        {
            return _context.Giacenze
                .FirstOrDefault(g => g.MagazzinoId == magazzinoId && g.MaterialeId == materialeId);
        }

        public void AggiornaGiacenza(int magazzinoId, int materialeId, int quantita)
        {
            var giacenza = GetGiacenza(magazzinoId, materialeId);
            if (giacenza == null)
            {
                giacenza = new Giacenza
                {
                    MagazzinoId = magazzinoId,
                    MaterialeId = materialeId,
                    QuantitaDisponibile = quantita
                };
                _context.Giacenze.Add(giacenza);
            }
            else
            {
                giacenza.QuantitaDisponibile += quantita;
            }

            _context.SaveChanges();
        }
    }
}
