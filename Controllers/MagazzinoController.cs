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
using MagazziniMaterialiAPI.Models.Entity.DTOs;

namespace MagazziniMaterialiAPI.Controllers
{
    [ApiController]
    [Route("Magazzino")]
    public class MagazzinoController : Controller
    {
        private readonly IMagazziniService _Magazziniervice;
        private readonly IMaterialeMagazziniService _MaterialeMagazziniervice;
        private readonly IMagazzinoMapper _MagazzinoMapper;

        public MagazzinoController(IMagazziniService Magazziniervice, IMaterialeMagazziniService MaterialeMagazziniervice, IMagazzinoMapper MagazzinoMapper)
        {
            _MaterialeMagazziniervice = MaterialeMagazziniervice;
            _Magazziniervice = Magazziniervice;
            _MagazzinoMapper = MagazzinoMapper;
        }

        /// <summary>
        /// get Magazzino list
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetMagazzini()
        {
            List<MagazzinoDTO> result = _Magazziniervice.GetAll();
            return Ok(result);
        }

        /// <summary>
        /// get Magazzino by id
        /// </summary>
        /// <param name="MagazzinoId"></param>
        /// <returns></returns>
        [HttpGet("{MagazzinoId}")]
        public ActionResult GetMagazzinoById([FromRoute] int MagazzinoId)
        {
            MagazzinoDTO? Magazzino = _Magazziniervice.GetById(MagazzinoId);
            return Magazzino == null ? NotFound() : Ok(Magazzino);
        }

        /// <summary>to Db
        /// add Magazzino 
        /// </summary>
        /// <param name="MagazzinoDTO"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddMagazzino([FromBody] MagazzinoDTO MagazzinoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Please Add Magazzino");
            }
            else
            {
                try
                {
                    Magazzino AddMagazzino = _Magazziniervice.AddMagazzino(MagazzinoDTO);
                    _Magazziniervice.SaveChanges();
                    MagazzinoDTO MagazzinoDTOResult = _MagazzinoMapper.MapToMagazzinoDTO(AddMagazzino);
                    return Ok(MagazzinoDTOResult);
                }
                catch (Exception e)
                {
                    return BadRequest(e);
                }
            }
        }

        /// <summary>
        /// edit Magazzino data in db
        /// </summary>
        /// <param name="MagazzinoId"></param>
        /// <param name="MagazzinoDTO"></param>
        /// <returns></returns>
        [HttpPut("{MagazzinoId}")]
        public ActionResult EditMagazzino([FromRoute] int MagazzinoId, [FromBody] MagazzinoDTO MagazzinoDTO)
        {

            bool isEdited = _Magazziniervice.EditMagazzino(MagazzinoId, MagazzinoDTO);
            if (!isEdited)
            {
                return NotFound("Magazzino Not Found!!!!!!");
            }

            _Magazziniervice.SaveChanges();
            return Ok(MagazzinoDTO);
        }

        /// <summary>
        /// delete Magazzino from db
        /// </summary>
        /// <param name="MagazzinoId"></param>
        /// <returns></returns>

        [HttpDelete("{MagazzinoId}")]
        public ActionResult DeleteMagazzino([FromRoute] int MagazzinoId)
        {
            bool isDeleted = _Magazziniervice.DeleteMagazzino(MagazzinoId);
            if (!isDeleted)
            {
                return NotFound("Magazzino Not Found!!!!!!");
            }
            _Magazziniervice.SaveChanges();
            return Ok("Magazzino Deleted Successfully");
        }

        [HttpPost("registerMaterialeMagazzino/{MagazzinoId}/{MaterialeId}")]
        public ActionResult RegisterMaterialeMagazzino([FromRoute] int MagazzinoId, [FromRoute] int MaterialeId)
        {

            MaterialeMagazzino MaterialeMagazzino = _MaterialeMagazziniervice.AddMaterialeMagazzino(MagazzinoId, MaterialeId);
            _MaterialeMagazziniervice.SaveChanges();
            return Ok("Materiale Registered to Magazzino Successfully");
        }

        [HttpDelete("registerMaterialeMagazzino/{MagazzinoId}/{MaterialeId}")]
        public ActionResult DeleteMaterialeMagazzino([FromRoute] int MagazzinoId, [FromRoute] int MaterialeId)
        {

            MaterialeMagazzino MaterialeMagazzino = _MaterialeMagazziniervice.AddMaterialeMagazzino(MagazzinoId, MaterialeId);
            _MaterialeMagazziniervice.SaveChanges();
            return Ok("Materiale Deleted from Magazzino Successfully");
        }

        /// <summary>
        /// get all Materiali in a Magazzino
        /// </summary>
        /// <param name="MagazzinoId"></param>
        /// <returns></returns>
        [HttpGet("MaterialiByMagazzino/{MagazzinoId}")]
        public ActionResult MaterialiByMagazzinoId([FromRoute] int MagazzinoId)
        {
            List<MaterialeDTO> Materiali = _Magazziniervice.GetMaterialiByMagazzinoId(MagazzinoId);
            return Ok(Materiali);
        }
    }
}
