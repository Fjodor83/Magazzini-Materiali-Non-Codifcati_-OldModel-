using MagazziniMaterialiAPI.Models.Entity;
using MagazziniMaterialiAPI.Models.Entity.DTOs;

namespace MagazziniMaterialiAPI
{
    public class MaterialeMapper : IMaterialeMapper
    {
        /// <summary>
        /// map MaterialeDTO to Materiale
        /// </summary>
        /// <param name="MaterialeDTO"></param>
        /// <returns></returns>
        public Materiale MapToMateriale(MaterialeDTO MaterialeDTO)
        {
            Materiale Materiale = new()
            {
                Id = MaterialeDTO.Id,
                CodiceMateriale = MaterialeDTO.CodiceMateriale,
                Descrizione = MaterialeDTO.Descrizione,
                DataCreazione = MaterialeDTO.DataCreazione,
                Note = MaterialeDTO.Note,
                Immagini = MaterialeDTO.Immagini,
            };

            return Materiale;

        }

        /// <summary>
        /// map Materiale to MaterialeDTO
        /// </summary>
        /// <param name="Materiale"></param>
        /// <returns></returns>
        public MaterialeDTO MapToMaterialeDTO(Materiale Materiale)
        {
            MaterialeDTO MaterialeDTO = new()
            {
                Id = Materiale.Id,
                CodiceMateriale = Materiale.CodiceMateriale,
                Descrizione = Materiale.Descrizione,
                DataCreazione = Materiale.DataCreazione,
                Note = Materiale.Note,
                Immagini = Materiale.Immagini,

            };

            return MaterialeDTO;

        }
    }
}