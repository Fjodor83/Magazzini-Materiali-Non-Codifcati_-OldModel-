using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MagazziniMaterialiAPI.Data;
using MagazziniMaterialiAPI.Models.Entity;

namespace MagazziniMaterialiAPI.Repositories
{
    public class MaterialeRepository : IMaterialeRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<MaterialeRepository> _logger;

        public MaterialeRepository(ApplicationDbContext context, ILogger<MaterialeRepository> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public Materiale GetByCodiceMateriale(string codiceMateriale)
        {
            return _context.Materiali
                .Include(m => m.Immagini)
                .Include(m => m.Classificazioni)
                .FirstOrDefault(m => m.CodiceMateriale == codiceMateriale);
        }

        public IEnumerable<Materiale> GetAll()
        {
            return _context.Materiali
                .Include(m => m.Immagini)
                .Include(m => m.Classificazioni)
                .ToList();
        }

        public void Add(Materiale materiale)
        {
            if (materiale == null)
            {
                throw new ArgumentNullException(nameof(materiale));
            }

            try
            {
                _context.Materiali.Add(materiale);
                _context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Errore durante il salvataggio del nuovo materiale: {Message}", ex.InnerException?.Message ?? ex.Message);
                throw new InvalidOperationException("Non è stato possibile salvare il materiale.", ex);
            }
        }

        public void Update(Materiale materiale)
        {
            if (materiale == null)
            {
                throw new ArgumentNullException(nameof(materiale));
            }

            var existingMateriale = _context.Materiali
                .Include(m => m.Immagini)
                .Include(m => m.Classificazioni)
                .FirstOrDefault(m => m.CodiceMateriale == materiale.CodiceMateriale);

            if (existingMateriale == null)
            {
                throw new KeyNotFoundException($"Il materiale con Codice Materiale {materiale.CodiceMateriale} non esiste.");
            }

            try
            {
                _context.Entry(existingMateriale).CurrentValues.SetValues(materiale);

                // Aggiorna o aggiungi le nuove immagini
                UpdateImmagini(existingMateriale, materiale.Immagini);

                // Aggiorna o aggiungi le nuove classificazioni
                UpdateClassificazioni(existingMateriale, materiale.Classificazioni);

                _context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Errore durante l'aggiornamento del materiale: {Message}", ex.InnerException?.Message ?? ex.Message);
                throw new InvalidOperationException("Non è stato possibile aggiornare il materiale.", ex);
            }
        }

        public void Delete(string codiceMateriale)
        {
            var materiale = _context.Materiali.FirstOrDefault(m => m.CodiceMateriale == codiceMateriale);
            if (materiale == null)
            {
                throw new KeyNotFoundException($"Il materiale con Codice Materiale {codiceMateriale} non esiste.");
            }

            try
            {
                _context.Materiali.Remove(materiale);
                _context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Errore durante l'eliminazione del materiale: {Message}", ex.InnerException?.Message ?? ex.Message);
                throw new InvalidOperationException("Non è stato possibile eliminare il materiale.", ex);
            }
        }

        public bool ExistsByCodice(string codiceMateriale)
        {
            if (string.IsNullOrEmpty(codiceMateriale))
            {
                throw new ArgumentNullException(nameof(codiceMateriale));
            }

            try
            {
                return _context.Materiali.Any(m => m.CodiceMateriale == codiceMateriale);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore durante la verifica dell'esistenza del materiale con codice: {CodiceMateriale}", codiceMateriale);
                throw new InvalidOperationException("Errore durante la verifica dell'esistenza del materiale.", ex);
            }
        }

        private void UpdateImmagini(Materiale existingMateriale, ICollection<MaterialeImmagine> newImmagini)
        {
            existingMateriale.Immagini.Clear();
            foreach (var immagine in newImmagini)
            {
                existingMateriale.Immagini.Add(immagine);
            }
        }

        private void UpdateClassificazioni(Materiale existingMateriale, ICollection<Classificazione> newClassificazioni)
        {
            existingMateriale.Classificazioni.Clear();
            foreach (var classificazione in newClassificazioni)
            {
                existingMateriale.Classificazioni.Add(classificazione);
            }
        }
    }
}