using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MagazziniMaterialiAPI.Data;
using MagazziniMaterialiAPI.Models.Entity;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace MagazziniMaterialiAPI.Repositories
{
    public class MaterialeRepository : BaseRepository, IMaterialeRepository
    {
        private ApplicationDbContext _ApplicationDbContext;
        public MaterialeRepository(ApplicationDbContext ApplicationDbContext) : base(ApplicationDbContext)
        {
            _ApplicationDbContext = ApplicationDbContext;
        }

        /// <summary>
        /// get all Materiali
        /// </summary>
        /// <returns></returns>
        public List<Materiale> GetAll()
        {
            return _ApplicationDbContext.Materiali.ToList();
        }

        /// <summary>
        /// get Materiale by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Materiale? GetById(int id)
        {
            return _ApplicationDbContext.Materiali.FirstOrDefault(g => g.Id == id);
        }
        /// <summary>
        /// delete Materiale
        /// </summary>
        /// <param name="Materiale"></param>
        public void DeleteMateriale(Materiale Materiale)
        {
            _ApplicationDbContext.Remove(Materiale);
        }

        /// <summary>
        /// edit Materiale data
        /// </summary>
        /// <param name="MaterialeId"></param>
        /// <param name="Materiale"></param>
        public bool EditMateriale(int MaterialeId, Materiale Materiale)
        {
            Materiale? existingEntity = _ApplicationDbContext.Materiali.Find(MaterialeId);
            if (existingEntity == null)
            {
                return false;
            }
            else
            {
                _ApplicationDbContext.Entry(existingEntity).State = EntityState.Detached;
            }
            _ApplicationDbContext.Attach(Materiale);
            _ApplicationDbContext.Entry(Materiale).State = EntityState.Modified;
            return true;
        }

        /// <summary>
        /// add Materiale to database
        /// </summary>
        /// <param name="Materiale"></param>
        /// <returns></returns>
        public Materiale AddMateriale(Materiale Materiale)
        {
            EntityEntry<Materiale> x = _ApplicationDbContext.Materiali.Add(Materiale);
            return x.Entity;

        }

        /// <summary>
        ///  get Materiale Magazzini by Materiale ID 
        /// </summary>
        /// <param name="MaterialeId"></param>
        /// <returns></returns>
        public List<Magazzino> GetMagazziniByMaterialeId(int MaterialeId)
        {
            List<Magazzino> Magazzini = new List<Magazzino>();
            Materiale? Materiale = _ApplicationDbContext.Materiali
                .Include(x => x.MaterialeMagazzini)
                .ThenInclude(x => x.Magazzino).FirstOrDefault(x => x.Id == MaterialeId);
            if (Materiale != null)
            {
                Magazzini = Materiale.MaterialeMagazzini.Select(x => x.Magazzino).ToList();
            }
            return Magazzini;
        }
    }
}
