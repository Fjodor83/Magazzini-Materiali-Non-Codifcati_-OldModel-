using MagazziniMaterialiAPI.Models.Entity;
using MagazziniMaterialiAPI.Models.Entity.DTOs;


namespace MagazziniMaterialiAPI
{
    public interface IMaterialiService
    {
        public List<MaterialeDTO> GetAll();
        public MaterialeDTO? GetById(int id);
        public bool EditMateriale(int MaterialeId, MaterialeDTO MaterialeDTO);
        public bool DeleteMateriale(int id);
        public Materiale AddMateriale(MaterialeDTO Materiale);
        public List<MagazzinoDTO> GetMagazziniByMaterialeId(int MaterialeId);
        public void SaveChanges();
       
    }
}
