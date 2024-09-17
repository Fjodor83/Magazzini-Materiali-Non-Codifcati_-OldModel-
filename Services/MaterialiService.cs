using MagazziniMaterialiAPI;
using MagazziniMaterialiAPI.Models.Entity;
using MagazziniMaterialiAPI.Models.Entity.DTOs;
using MagazziniMaterialiAPI.Repositories;
namespace MagazziniMaterialiAPI.Services
{
    public class MaterialiService : IMaterialiService
    {
        private readonly IMaterialeRepository _MaterialeRepository;
        private readonly IMaterialeMapper _MaterialeMapper;
        private readonly IMagazzinoMapper _MagazzinoMapper;
        public MaterialiService(IMaterialeRepository MaterialeRepository, IMaterialeMapper MaterialeMapper, IMagazzinoMapper MagazzinoMapper)
        {
            _MaterialeRepository = MaterialeRepository;
            _MaterialeMapper = MaterialeMapper;
            _MagazzinoMapper = MagazzinoMapper;
        }

        /// <summary>
        /// get list of Materiali DTO
        /// </summary>
        /// <returns></returns>
        public List<MaterialeDTO> GetAll()
        {
            List<MaterialeDTO> Materiali = new List<MaterialeDTO>();
            Materiali = _MaterialeRepository.GetAll().Select(g => _MaterialeMapper.MapToMaterialeDTO(g)).ToList();
            return Materiali;
        }

        /// <summary>
        /// get MaterialeDTO by Materiale Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MaterialeDTO? GetById(int id)
        {
            Materiale? Materiale = _MaterialeRepository.GetById(id);
            if (Materiale == null)
            {
                return null;
            }
            MaterialeDTO MaterialeDTO = _MaterialeMapper.MapToMaterialeDTO(Materiale);
            return MaterialeDTO;
        }

        /// <summary>
        /// add Materiale to db
        /// </summary>
        /// <param name="Materiale"></param>
        /// <returns></returns>
        public Materiale AddMateriale(MaterialeDTO Materiale)
        {
            Materiale MaterialeEntity = _MaterialeMapper.MapToMateriale(Materiale);
            return _MaterialeRepository.AddMateriale(MaterialeEntity);
        }

        /// <summary>
        /// delete Materiale 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DeleteMateriale(int id)
        {
            Materiale? Materiale = _MaterialeRepository.GetById(id); ;
            if (Materiale == null)
            {
                return false;
            }
            _MaterialeRepository.DeleteMateriale(Materiale);
            return true;

        }

        /// <summary>
        /// edit Materiale data 
        /// </summary>
        /// <param name="MaterialeId"></param>
        /// <param name="MaterialeDTO"></param>
        /// <returns></returns>
        public bool EditMateriale(int MaterialeId, MaterialeDTO MaterialeDTO)
        {
            Materiale MaterialeEntity = _MaterialeMapper.MapToMateriale(MaterialeDTO);
            return _MaterialeRepository.EditMateriale(MaterialeId, MaterialeEntity);
        }
        /// <summary>
        ///  get Materiale Magazzini by Materiale ID 
        /// </summary>
        /// <param name="MaterialeId"></param>
        /// <returns></returns>
        public List<MagazzinoDTO> GetMagazziniByMaterialeId(int MaterialeId)
        {
            {
                return _MaterialeRepository.GetMagazziniByMaterialeId(MaterialeId).Select(g => _MagazzinoMapper.MapToMagazzinoDTO(g)).ToList();
            }
        }
        /// <summary>
        /// save changes to DB
        /// </summary>
        public void SaveChanges()
        {
            _MaterialeRepository.SaveChanges();
        }

    }
}