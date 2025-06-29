using MagicVilla_API.Datos;
using MagicVilla_API.DTO;
using MagicVilla_API.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillaController : ControllerBase
    {
        // * Logger
        private readonly ILogger<VillaController> _logger;
        private readonly AplicationDBContext _db;
        public VillaController(ILogger<VillaController> logger, AplicationDBContext db) => (_logger, _db) = (logger, db);

        [HttpGet]
        [ProducesResponseType(200)]
        public ActionResult<IEnumerable<VillaDTO>> GetVillas()
        {
            _logger.LogInformation("Getting all villas");
            return Ok(_db.Villas.ToList());
        }

        [HttpGet("id:int", Name = "GetVilla")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public ActionResult<VillaDTO> GetVilla(int id)
        {
            if (id is 0)
            {
                _logger.LogError($"Get Villa Error with id: {id}");
                return BadRequest();
            }
            // var data = VillaStore.villaList.FirstOrDefault(v => v.Id == id);
            var villa = _db.Villas.FirstOrDefault(v => v.Id == id);
            return villa != null ? Ok(villa) : NotFound();
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public ActionResult<VillaDTO> CreateVilla([FromBody] VillaDTO villaDTO)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            // if (VillaStore.villaList.FirstOrDefault(v => v.Name.ToLower() == villaDTO.Name.ToLower()) != null) return BadRequest("Villa already exists!");
            if (_db.Villas.FirstOrDefault(v => v.Name.ToLower() == villaDTO.Name.ToLower()) != null) return BadRequest("Villa already exists!");
            if (villaDTO is null) return BadRequest(villaDTO);
            if (villaDTO.Id > 0) return StatusCode(500);
            // villaDTO.Id = VillaStore.villaList.Count + 1;
            // VillaStore.villaList.Add(villaDTO);
            // ? Creating a new Villa Model
            Villa model = new()
            {
                Name = villaDTO.Name,
                Details = villaDTO.Details,
                ImageUrl = villaDTO.ImageUrl,
                Occupants = villaDTO.Occupants,
                Tariff = villaDTO.Tariff,
                SquareMeters = villaDTO.SquareMeters,
                Amenity = villaDTO.Amenity,
            };
            _db.Villas.Add(model);
            _db.SaveChanges();
            return CreatedAtRoute("GetVilla", new { id = villaDTO.Id }, villaDTO);
        }

        [HttpDelete("id:int", Name = "DeleteVilla")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteVilla(int id)
        {
            if (id is 0) return BadRequest();
            // var villa = VillaStore.villaList.FirstOrDefault(v => v.Id == id);
            var villa = _db.Villas.FirstOrDefault(v => v.Id == id);
            if (villa is null) return NotFound();
            // VillaStore.villaList.Remove(villa);
            _db.Villas.Remove(villa);
            _db.SaveChanges();
            return NoContent();
        }

        [HttpPut("id:int")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public ActionResult<VillaDTO> UpdateVilla(int id, [FromBody] VillaDTO villaDTO)
        {
            if (villaDTO is null || id != villaDTO.Id) return BadRequest();
            /*
            var villa = VillaStore.villaList.FirstOrDefault(v => v.Id == id)!;
            villa.Name = villaDTO.Name;
            villa.Occupants = villaDTO.Occupants;
            villa.SquareMeters = villaDTO.SquareMeters;
            */
            // ? Add new Villa Model
            Villa model = new()
            {
                Id = villaDTO.Id,
                Name = villaDTO.Name,
                Details = villaDTO.Details,
                ImageUrl = villaDTO.ImageUrl,
                Occupants = villaDTO.Occupants,
                Tariff = villaDTO.Tariff,
                SquareMeters = villaDTO.SquareMeters,
                Amenity = villaDTO.Amenity,
            };
            _db.Villas.Update(model);
            _db.SaveChanges();
            return CreatedAtRoute("GetVilla", new { id = villaDTO.Id }, villaDTO);
        }

        [HttpPatch("id:int")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdatePartialVilla(int id, JsonPatchDocument<VillaDTO> patchDto)
        {
            if (patchDto is null || id is 0) return BadRequest();
            // var villa = VillaStore.villaList.FirstOrDefault(v => v.Id == id)!;
            var villa = _db.Villas.AsNoTracking().FirstOrDefault(v => v.Id == id)!;
            VillaDTO villaDTO = new()
            {
                Id = villa.Id,
                Name = villa.Name,
                Details = villa.Details,
                ImageUrl = villa.ImageUrl,
                Occupants = villa.Occupants,
                Tariff = villa.Tariff,
                SquareMeters = villa.SquareMeters,
                Amenity = villa.Amenity
            };
            if (villa is null) return BadRequest();
            patchDto.ApplyTo(villaDTO, ModelState);
            if (!ModelState.IsValid) return BadRequest(ModelState);
            Villa model = new()
            {
                Id = villaDTO.Id,
                Name = villaDTO.Name,
                Details = villaDTO.Details,
                ImageUrl = villaDTO.ImageUrl,
                Occupants = villaDTO.Occupants,
                Tariff = villaDTO.Tariff,
                SquareMeters = villaDTO.SquareMeters,
                Amenity = villaDTO.Amenity
            };
            _db.Villas.Update(model);
            _db.SaveChanges();
            return NoContent();
        }
    }
}
