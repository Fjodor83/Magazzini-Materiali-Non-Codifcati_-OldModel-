using MagazziniMaterialiAPI.Models.Entity;
using MagazziniMaterialiAPI.Models.Entity.DTOs;


namespace MagazziniMaterialiAPI.Services
{
    public interface IMaterialiService
    {
        public List<MaterialeDTO> GetAll();
        public MaterialeDTO? GetById(int id);
        public bool EditMateriale(string codiceMateriale, MaterialeDTO MaterialeDTO);
        public bool DeleteMateriale(int id);
        public Materiale AddMateriale(MaterialeDTO Materiale);
        public List<MagazzinoDTO> GetMagazziniByMaterialeId(int MaterialeId);
        public void SaveChanges();
        bool ExistsByCodice(string codiceMateriale);
        MaterialeDTO? GetByCodiceMateriale(string codiceMateriale);
    }
}
