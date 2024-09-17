
using MagazziniMaterialiAPI.Data;
using MagazziniMaterialiAPI.Models.Entity;

namespace MagazziniMaterialiAPI.Repositories
{
    public interface IMaterialeMagazzinoRepository : IBaseRepository
    {
        MaterialeMagazzino? GetMaterialeMagazzino(int MaterialeId, int MagazzinoId);
        public MaterialeMagazzino AddMaterialeMagazzino(MaterialeMagazzino MaterialeMaterialeMagazzino);
        public void DeleteMaterialeMagazzino(MaterialeMagazzino MaterialeMaterialeMagazzino);
    }
}
