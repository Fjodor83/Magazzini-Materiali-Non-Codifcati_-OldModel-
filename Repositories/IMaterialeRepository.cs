using MagazziniMaterialiAPI.Models.Entity;

namespace MagazziniMaterialiAPI.Repositories
{
    public interface IMaterialeRepository
    {
        Materiale GetById(int id);
        IEnumerable<Materiale> GetAll();
        void Add(Materiale materiale);  
        void Update(Materiale materiale);

    }
}
