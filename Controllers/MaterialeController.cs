using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using QRCoder;
using MagazziniMaterialiAPI.Models.Entity;
using MagazziniMaterialiAPI.Repositories;
using MagazziniMaterialiAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace MagazziniMaterialiAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MaterialeController : ControllerBase
    {
        private readonly EtichettaService _etichettaService;
        private readonly IMaterialeRepository _materialeRepository;
        private readonly ILogger<MaterialeController> _logger;

        public MaterialeController(EtichettaService etichettaService, IMaterialeRepository materialeRepository, ILogger<MaterialeController> logger)
        {
            _etichettaService = etichettaService ?? throw new ArgumentNullException(nameof(etichettaService));
            _materialeRepository = materialeRepository ?? throw new ArgumentNullException(nameof(materialeRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        public ActionResult<IEnumerable<Materiale>> GetListaMateriali()
        {
            try
            {
                var materiali = _materialeRepository.GetAll();
                return Ok(materiali);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore durante il recupero della lista dei materiali");
                return StatusCode(500, "Si è verificato un errore interno durante il recupero della lista dei materiali.");
            }
        }

        [HttpPost]
        public ActionResult<Materiale> CreaMateriale([FromBody] Materiale nuovoMateriale)
        {
            if (nuovoMateriale == null)
            {
                return BadRequest("Il materiale fornito non è valido.");
            }

            if (_materialeRepository.ExistsByCodice(nuovoMateriale.CodiceMateriale))
            {
                return Conflict($"Un materiale con il codice '{nuovoMateriale.CodiceMateriale}' esiste già.");
            }

            try
            {
                string qrCodeData = GeneraCodiceQR(nuovoMateriale.CodiceMateriale);

                var immagineQRCode = new MaterialeImmagine
                {
                    UrlImmagine = string.Empty,
                    IsPrincipale = true,
                    QRCodeData = qrCodeData
                };

                nuovoMateriale.Immagini = new List<MaterialeImmagine> { immagineQRCode };
                nuovoMateriale.DataCreazione = DateTime.UtcNow;

                _materialeRepository.Add(nuovoMateriale);

                return CreatedAtAction(nameof(GetMateriale), new { codiceMateriale = nuovoMateriale.CodiceMateriale }, nuovoMateriale);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore durante la creazione del materiale");
                return StatusCode(500, "Si è verificato un errore interno durante la creazione del materiale.");
            }
        }

        [HttpGet("{codiceMateriale}")]
        public ActionResult<Materiale> GetMateriale(string codiceMateriale)
        {
            var materiale = _materialeRepository.GetByCodiceMateriale(codiceMateriale);

            if (materiale == null)
            {
                return NotFound($"Materiale con codice '{codiceMateriale}' non trovato.");
            }

            return Ok(materiale);
        }

        [HttpPost("{codiceMateriale}/immagini")]
        public ActionResult<Materiale> AggiungiImmagine(string codiceMateriale, [FromBody] MaterialeImmagine nuovaImmagine)
        {
            var materiale = _materialeRepository.GetByCodiceMateriale(codiceMateriale);
            if (materiale == null)
            {
                return NotFound($"Materiale con codice '{codiceMateriale}' non trovato.");
            }

            if (materiale.Immagini == null)
            {
                materiale.Immagini = new List<MaterialeImmagine>();
            }
            materiale.Immagini.Add(nuovaImmagine);

            try
            {
                _materialeRepository.Update(materiale);
                return Ok(materiale);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore durante l'aggiunta dell'immagine al materiale");
                return StatusCode(500, "Si è verificato un errore interno durante l'aggiunta dell'immagine.");
            }
        }

        [HttpDelete("{codiceMateriale}")]
        public IActionResult Delete(string codiceMateriale)
        {
            try
            {
                var materiale = _materialeRepository.GetByCodiceMateriale(codiceMateriale);
                if (materiale == null)
                {
                    return NotFound($"Materiale con codice '{codiceMateriale}' non trovato.");
                }

                _materialeRepository.Delete(codiceMateriale);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogError(ex, "Materiale con codice {CodiceMateriale} non trovato.", codiceMateriale);
                return NotFound(ex.Message);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Errore di aggiornamento del database durante l'eliminazione del materiale con codice {CodiceMateriale}.", codiceMateriale);
                return StatusCode(500, "Errore di aggiornamento del database.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore durante l'eliminazione del materiale con codice {CodiceMateriale}.", codiceMateriale);
                return StatusCode(500, "Si è verificato un errore interno durante l'eliminazione del materiale.");
            }
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