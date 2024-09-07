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

        public Materiale GetById(int id)
        {
            return _context.Materiali
                .Include(m => m.Immagini)
                .FirstOrDefault(m => m.Id == id);
        }

        public IEnumerable<Materiale> GetAll()
        {
            return _context.Materiali
                .Include(m => m.Immagini)
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
                .FirstOrDefault(m => m.Id == materiale.Id);

            if (existingMateriale == null)
            {
                throw new KeyNotFoundException($"Il materiale con ID {materiale.Id} non esiste.");
            }

            try
            {
                _context.Entry(existingMateriale).CurrentValues.SetValues(materiale);

                // Rimuovi le immagini che non sono più presenti
                existingMateriale.Immagini = existingMateriale.Immagini
                    .Where(i => materiale.Immagini.Any(mi => mi.Id == i.Id))
                    .ToList();

                // Aggiorna o aggiungi le nuove immagini
                foreach (var immagine in materiale.Immagini)
                {
                    var existingImmagine = existingMateriale.Immagini.FirstOrDefault(i => i.Id == immagine.Id);
                    if (existingImmagine != null)
                    {
                        _context.Entry(existingImmagine).CurrentValues.SetValues(immagine);
                    }
                    else
                    {
                        existingMateriale.Immagini.Add(immagine);
                    }
                }

                _context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Errore durante l'aggiornamento del materiale: {Message}", ex.InnerException?.Message ?? ex.Message);
                throw new InvalidOperationException("Non è stato possibile aggiornare il materiale.", ex);
            }
        }

        public void Delete(int id)
        {
            var materiale = _context.Materiali.Find(id);
            if (materiale == null)
            {
                throw new KeyNotFoundException($"Il materiale con ID {id} non esiste.");
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
    }
}