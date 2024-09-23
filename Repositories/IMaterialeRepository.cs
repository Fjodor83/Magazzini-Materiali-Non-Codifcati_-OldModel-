using MagazziniMaterialiAPI.Data;
using MagazziniMaterialiAPI.Models.Entity;

namespace MagazziniMaterialiAPI.Repositories
{
    public interface IMaterialeRepository : IBaseRepository
    {
        public List<Materiale> GetAll();
        public Materiale? GetById(int id);

        public Materiale AddMateriale(Materiale Materiale);
        bool EditMateriale(string codiceMateriale, Materiale Materiale);
        public void DeleteMateriale(Materiale Materiale);
        public List<Magazzino> GetMagazziniByMaterialeId(int MaterialeId);
        bool ExistsByCodice(string codiceMateriale);
        Materiale? GetByCodiceMateriale(string codiceMateriale);
    }
}
