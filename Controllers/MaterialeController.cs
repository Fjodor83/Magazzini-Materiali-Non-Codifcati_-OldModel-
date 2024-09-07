using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using QRCoder;
using MagazziniMaterialiAPI.Models.Entity;
using MagazziniMaterialiAPI.Repositories;
using MagazziniMaterialiAPI.Services;

namespace MagazziniMaterialiAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MaterialeController : ControllerBase
    {
        private readonly EtichettaService _etichettaService;
        private readonly IMaterialeRepository _materialeRepository;

        public MaterialeController(EtichettaService etichettaService, IMaterialeRepository materialeRepository)
        {
            _etichettaService = etichettaService;
            _materialeRepository = materialeRepository;
        }

        // Creazione di un nuovo materiale con QR Code
        [HttpPost("crea-materiale")]
        public IActionResult CreaMateriale([FromBody] Materiale nuovoMateriale)
        {
            if (nuovoMateriale == null)
            {
                return BadRequest("Materiale non valido.");
            }

            // Genera il codice QR
            string qrCodeData = GeneraCodiceQR(nuovoMateriale.CodiceMateriale);

            // Aggiunge il QR Code come immagine principale
            var immagineQRCode = new MaterialeImmagine
            {
                UrlImmagine = "", // URL vuoto, può essere gestito come necessario
                IsPrincipale = true,
                QRCodeData = qrCodeData
            };

            nuovoMateriale.Immagini = new List<MaterialeImmagine> { immagineQRCode };
            nuovoMateriale.DataCreazione = DateTime.Now;

            // Salva il materiale
            _materialeRepository.Add(nuovoMateriale);

            return Ok(nuovoMateriale);
        }

        // Endpoint per aggiungere immagini a un materiale esistente
        [HttpPost("aggiungi-immagine/{id}")]
        public IActionResult AggiungiImmagine(int id, [FromBody] MaterialeImmagine nuovaImmagine)
        {
            var materiale = _materialeRepository.GetById(id);
            if (materiale == null)
            {
                return NotFound("Materiale non trovato.");
            }

            // Aggiungi l'immagine al materiale
            if (materiale.Immagini == null)
            {
                materiale.Immagini = new List<MaterialeImmagine>();
            }
            materiale.Immagini.Add(nuovaImmagine);

            _materialeRepository.Update(materiale);

            return Ok(materiale);
        }

        // Funzione per generare il QR Code
        private string GeneraCodiceQR(string codiceMateriale)
        {
            using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
            {
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(codiceMateriale, QRCodeGenerator.ECCLevel.Q);
                BitmapByteQRCode qrCode = new BitmapByteQRCode(qrCodeData);
                byte[] qrCodeImage = qrCode.GetGraphic(20);
                return Convert.ToBase64String(qrCodeImage);
            }
        }
    }
}