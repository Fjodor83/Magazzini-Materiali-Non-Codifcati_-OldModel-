using MagazziniMaterialiAPI.Data;
using MagazziniMaterialiAPI.Models.Entity;

namespace MagazziniMaterialiAPI.Repositories
{
    public interface IGiacenzaRepository
    {
        Giacenza GetGiacenza(int magazzinoId, int materialeId);
        void AggiornaGiacenza(int magazzinoId, int materialeId, int quantita);
    }

}
