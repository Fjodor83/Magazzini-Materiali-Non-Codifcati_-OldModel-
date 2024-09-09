namespace MagazziniMaterialiAPI.Models.Entity.DTOs
{
    public class MovimentazioneIngressoDto
    {
        public int MaterialeId { get; set; }
        public int MagazzinoId { get; set; }
        public int Quantita { get; set; }
        public string Nota { get; set; }
    }
}
