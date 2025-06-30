using System.Net;
using AutoMapper;
using MagicVilla_API.DTO;
using MagicVilla_API.Models;
using MagicVilla_API.Repository.IRepository;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace MagicVilla_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillaNumberController : ControllerBase
    {
        // * Logger
        private readonly ILogger<VillaNumberController> _logger;
        private readonly IVillaRepository _villaRepo;
        private readonly IVillaNumberRepository _numberRepo;
        private readonly IMapper _mapper;
        protected ApiRespose _response;

        public VillaNumberController(ILogger<VillaNumberController> logger, IVillaRepository villaRepo, IVillaNumberRepository numberRepo, IMapper mapper)
        {
            _logger = logger;
            _villaRepo = villaRepo;
            _numberRepo = numberRepo;
            _mapper = mapper;
            _response = new();
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<ActionResult<ApiRespose>> GetNumberVillas()
        {
            try
            {
                _logger.LogInformation("Getting all number villas");
                IEnumerable<VillaNumber> get_nvillas = await _numberRepo.GetAll();
                _response.Result = _mapper.Map<IEnumerable<NumberVillaDto>>(get_nvillas);
                _response.statusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccessful = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return _response;
        }

        [HttpGet("id:int", Name = "GetVillaNumber")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<ApiRespose>> GetVillaNumber(int id)
        {
            try
            {
                if (id is 0)
                {
                    string message = $"Get Villa Number Error with id: {id}";
                    _logger.LogError(message);
                    _response.statusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccessful = false;
                    return BadRequest(_response);
                }
                var numberVilla = await _numberRepo.Get(v => v.VillaNo == id);
                if (numberVilla is null)
                {
                    _response.statusCode = HttpStatusCode.NotFound;
                    _response.IsSuccessful = false;
                    return NotFound(_response);
                }
                _response.Result = _mapper.Map<VillaDTO>(numberVilla);
                _response.statusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {

                _response.IsSuccessful = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return _response;
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<ApiRespose>> CreateVillaNumber([FromBody] NumberVillaCreateDto createDTO)
        {
            try
            {

                if (!ModelState.IsValid) return BadRequest(ModelState);
                if (await _numberRepo.Get(v => v.VillaNo == createDTO.VillaNo) != null)
                {
                    ModelState.AddModelError("CustomError", "The Villa ID doesn't exist");
                    return BadRequest(ModelState);
                }

                if (await _villaRepo.Get(v => v.Id == createDTO.VillaId) == null)
                {
                    ModelState.AddModelError("CustomError", "Villa ID is Invalid!");
                    return BadRequest(ModelState);
                }

                if (createDTO is null) return BadRequest(createDTO);

                VillaNumber model = _mapper.Map<VillaNumber>(createDTO);

                DateTime now = DateTime.Now;
                DateOnly get_date = new DateOnly(now.Year, now.Month, now.Day);
                model.CreatedDate = get_date;
                model.UpdateDate = get_date;

                await _numberRepo.Create(model);

                _response.Result = model;
                _response.statusCode = HttpStatusCode.Created;
                return CreatedAtRoute("GetVillaNumber", new { id = model.VillaNo }, _response);
            }
            catch (Exception ex)
            {
                _response.IsSuccessful = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return _response;
        }

        [HttpDelete("id:int")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteVillaNumber(int id)
        {
            try
            {
                if (id is 0)
                {
                    _response.IsSuccessful = false;
                    _response.statusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var numberVilla = await _numberRepo.Get(v => v.VillaNo == id);
                if (numberVilla is null)
                {
                    _response.IsSuccessful = false;
                    _response.statusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                await _numberRepo.Remove(numberVilla);
                _response.statusCode = HttpStatusCode.NoContent;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccessful = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return BadRequest(_response);
        }

        [HttpPut("id:int")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateVillaNumber(int id, [FromBody] NumberVillaUpdateDto updateDTO)
        {
            if (updateDTO is null || id != updateDTO.VillaNo)
            {
                _response.IsSuccessful = false;
                _response.statusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }
            if (await _villaRepo.Get(v => v.Id == updateDTO.VillaId) == null)
            {
                ModelState.AddModelError("CustomError", "Villa ID is Invalid!");
                return BadRequest(ModelState);
            }
            VillaNumber model = _mapper.Map<VillaNumber>(updateDTO);
            await _numberRepo.Update(model);
            _response.statusCode = HttpStatusCode.NoContent;
            return Ok(_response);
        }
    }
}
