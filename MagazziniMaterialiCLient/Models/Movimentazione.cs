﻿namespace MagazziniMaterialiCLient.Models
{
    public class Movimentazione
    {
        public int Id { get; set; }
        public string TipoMovimentazione { get; set; }
        public int MaterialeId { get; set; }
        public Materiale Materiale { get; set; }
        public int MagazzinoId { get; set; }
        public Magazzino Magazzino { get; set; }
        public int Quantita { get; set; }
        public DateTime DataMovimentazione { get; set; }
        public string Nota { get; set; }
    }
}
