using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MagazziniMaterialiAPI.Data;
using MagazziniMaterialiAPI.Models.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace MagazziniMaterialiAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MagazzinoController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
       

        public MagazzinoController(ApplicationDbContext context)
        {
            _context = context;
        }

        

        // GET: api/Magazzino
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Magazzino>>> GetMagazzini()
        {
            return await _context.Magazzini.ToListAsync();
        }

        // GET: api/Magazzino/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<Magazzino>> GetMagazzino(int id)
        {
            var magazzino = await _context.Magazzini.FindAsync(id);

            if (magazzino == null)
            {
                return NotFound();
            }

            return magazzino;
        }

        // PUT: api/Magazzino/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutMagazzino(int id, Magazzino magazzino)
        {
            if (id != magazzino.Id)
            {
                return BadRequest();
            }

            _context.Entry(magazzino).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MagazzinoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Magazzino
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles ="Admin")]
        public async Task<ActionResult<Magazzino>> PostMagazzino(Magazzino magazzino)
        {
            _context.Magazzini.Add(magazzino);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMagazzino", new { id = magazzino.Id }, magazzino);
        }

        // DELETE: api/Magazzino/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> DeleteMagazzino(int id)
        {
            var magazzino = await _context.Magazzini.FindAsync(id);
            if (magazzino == null)
            {
                return NotFound();
            }

            _context.Magazzini.Remove(magazzino);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MagazzinoExists(int id)
        {
            return _context.Magazzini.Any(e => e.Id == id);
        }
    }
}
