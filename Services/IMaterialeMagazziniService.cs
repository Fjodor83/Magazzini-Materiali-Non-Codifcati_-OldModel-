using MagazziniMaterialiAPI.Models.Entity;


namespace MagazziniMaterialiAPI
{
    public interface IMaterialeMagazziniService
    {
        public bool DeleteMaterialeMagazzino(int MagazzinoId, int MaterialeId);
        public MaterialeMagazzino AddMaterialeMagazzino(int MagazzinoId, int MaterialeId);
        public void SaveChanges();

    }
}
