using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using QRCoder;
using MagazziniMaterialiAPI.Models.Entity;
using MagazziniMaterialiAPI.Models.Entity.DTOs;
using MagazziniMaterialiAPI.Services;

namespace MagazziniMaterialiAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MaterialeController : ControllerBase
    {
        private readonly IMaterialiService _materialiService;
        private readonly IMaterialeMapper _materialeMapper;
        private readonly EtichettaService _etichettaService;
        private readonly ILogger<MaterialeController> _logger;

        public MaterialeController(IMaterialiService materialiService, IMaterialeMapper materialeMapper, EtichettaService etichettaService, ILogger<MaterialeController> logger)
        {
            _materialiService = materialiService ?? throw new ArgumentNullException(nameof(materialiService));
            _materialeMapper = materialeMapper ?? throw new ArgumentNullException(nameof(materialeMapper));
            _etichettaService = etichettaService ?? throw new ArgumentNullException(nameof(etichettaService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        public ActionResult<IEnumerable<MaterialeDTO>> GetMateriali()
        {
            var result = _materialiService.GetAll();
            return Ok(result);
        }

        [HttpGet("{materialeId:int}")]
        public ActionResult<MaterialeDTO> GetMaterialeById(int materialeId)
        {
            var materiale = _materialiService.GetById(materialeId);
            if (materiale == null) return NotFound();
            return Ok(materiale);
        }

        [HttpGet("{codiceMateriale}")]
        public ActionResult<MaterialeDTO> GetMaterialeByCodiceMateriale(string codiceMateriale)
        {
            var materiale = _materialiService.GetByCodiceMateriale(codiceMateriale);
            if (materiale == null) return NotFound($"Materiale con codice '{codiceMateriale}' non trovato.");
            return Ok(materiale);
        }

        [HttpPost]
        public ActionResult<MaterialeDTO> AddMateriale([FromBody] MaterialeDTO materialeDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                // Verifica se esiste già un materiale con lo stesso codice
                if (_materialiService.ExistsByCodice(materialeDTO.CodiceMateriale))
                {
                    return Conflict($"Un materiale con il codice '{materialeDTO.CodiceMateriale}' esiste già.");
                }

                // Aggiungi il materiale
                var nuovoMateriale = _materialiService.AddMateriale(materialeDTO);

                // Genera il codice QR
                string qrCodeData = GeneraCodiceQR(nuovoMateriale.CodiceMateriale);

                // Crea l'immagine del QRCode
                var immagineQRCode = new MaterialeImmagine
                {
                    UrlImmagine = string.Empty,
                    IsPrincipale = true,
                    QRCodeData = qrCodeData
                };

                // Assegna la lista di immagini al materiale
                nuovoMateriale.Immagini = new List<MaterialeImmagine> { immagineQRCode };
                nuovoMateriale.DataCreazione = DateTime.UtcNow;

                // Salva le modifiche nel database
                _materialiService.SaveChanges();

                // Mappa il materiale al DTO e restituisci il risultato
                var materialeDTOResult = _materialeMapper.MapToMaterialeDTO(nuovoMateriale);
                return CreatedAtAction(nameof(GetMaterialeById), new { materialeId = nuovoMateriale.Id }, materialeDTOResult);
            }
            catch (Exception ex)
            {
                // Log dell'errore e restituzione di un errore 500
                _logger.LogError(ex, "Errore durante la creazione del materiale: {Message}", ex.Message);
                return StatusCode(500, "Si è verificato un errore interno durante la creazione del materiale.");
            }
        }


        [HttpPut("{codiceMateriale}")]
        public ActionResult EditMateriale(string codiceMateriale, [FromBody] MaterialeDTO materialeDTO)
        {
            var isEdited = _materialiService.EditMateriale(codiceMateriale, materialeDTO);
            if (!isEdited) return NotFound("Materiale non trovato.");

            _materialiService.SaveChanges();
            return Ok(materialeDTO);
        }

        [HttpDelete("{materialeId}")]
        public ActionResult DeleteMateriale(int materialeId)
        {
            var isDeleted = _materialiService.DeleteMateriale(materialeId);
            if (!isDeleted) return NotFound("Materiale non trovato.");

            _materialiService.SaveChanges();
            return Ok("Materiale eliminato correttamente.");
        }

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