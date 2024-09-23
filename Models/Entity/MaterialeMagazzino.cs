using System.Text.Json.Serialization;

namespace MagazziniMaterialiAPI.Models.Entity
{
    public class MaterialeMagazzino
    {
        public int MaterialeMagazzinoID { get; set; }
        public string CodiceMateriale { get; set; }
        public Materiale Materiale { get; set; }
        public int MagazzinoID { get; set; }
        [JsonIgnore]
        public Magazzino Magazzino { get; set; }

        public MaterialeMagazzino()
        {

        }
    }
}