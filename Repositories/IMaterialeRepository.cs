using MagazziniMaterialiAPI.Models.Entity;

namespace MagazziniMaterialiAPI.Repositories
{
    public interface IMaterialeRepository
    {
        Materiale GetByCodiceMateriale(string codiceMateriale);
        IEnumerable<Materiale> GetAll();
        void Add(Materiale materiale);  
        void Update(Materiale materiale);
        bool ExistsByCodice(string codiceMateriale);
        void Delete(string codiceMateriale);
    }
}
