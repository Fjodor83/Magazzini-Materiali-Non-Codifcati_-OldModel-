using MagazziniMaterialiAPI.Data;
using MagazziniMaterialiAPI.Models.Entity;

namespace MagazziniMaterialiAPI.Repositories
{
    public interface IMaterialeRepository : IBaseRepository
    {
        public List<Materiale> GetAll();
        public Materiale? GetById(int id);

        public Materiale AddMateriale(Materiale Materiale);
        bool EditMateriale(int MaterialeId, Materiale Materiale);
        public void DeleteMateriale(Materiale Materiale);
        public List<Magazzino> GetMagazziniByMaterialeId(int MaterialeId);

    }
}
