using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Tennis_Court_Booking_API.Models;
using Tennis_Court_Booking_API.Models.DTO;
using Tennis_Court_Booking_API.Repository.IRepository;

namespace Tennis_Court_Booking_API.Controllers
{
    [Route("api/TennisCourts")]
    [ApiController]

    public class TennisCourtsController : ControllerBase
    {

        private readonly ITennisCourtRepository _tennisCourtRepository;
        private readonly IMapper _mapper;
        protected APIResponse _response;
        public TennisCourtsController(ITennisCourtRepository tennisCourtRepository, IMapper mapper)
        {
            _tennisCourtRepository = tennisCourtRepository;
            _mapper = mapper;
            _response = new APIResponse();
        }
        [HttpGet]
        [ResponseCache(CacheProfileName = "Default30")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public  async Task<ActionResult<APIResponse>> GetAllTennisCourts()
        {
            try
            {
                IEnumerable<TennisCourt> tennisCourtLists = await _tennisCourtRepository.GetAllAsync();
                _response.Result = _mapper.Map<List<TennisCourtDto>>(tennisCourtLists);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessage
                    = new List<string> { ex.ToString() };
            }
            return _response;
        }

        [HttpGet("{id:int}", Name ="GetTennisCourtDetail")]
        [ResponseCache(CacheProfileName = "Default30")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> GetTennisCourt( int id)
        {
            try
                {
                if (id == 0)
                {
                    _response.IsSuccess = false;
                    return BadRequest();

                }

                var tennisCourt = await _tennisCourtRepository.GetAsync(u => u.Id == id);
                if (tennisCourt == null)
                {
                    _response.IsSuccess = false;
                    return NotFound();

                }
                _response.Result = _mapper.Map<TennisCourtDto>(tennisCourt);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessage
                    = new List<string> { ex.ToString() };
            }
            return _response;

        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> CreateTennisCourt([FromBody] TennisCourtCreateDto createTennisCourtDto)
        {
            try
            {
                if (await _tennisCourtRepository.GetAsync(u => u.Name.ToLower() == createTennisCourtDto.Name.ToLower()) != null)
                {
                    ModelState.AddModelError("CustomError", "TennisCourtAlreadyExists");
                    _response.IsSuccess = false;
                    return BadRequest(ModelState);
                }

                if (createTennisCourtDto == null)
                {
                    _response.IsSuccess = false;
                    return BadRequest(createTennisCourtDto);
                }

                TennisCourt tennisCourt = _mapper.Map<TennisCourt>(createTennisCourtDto);

                await _tennisCourtRepository.CreateAsync(tennisCourt);

                _response.Result = _mapper.Map<TennisCourtCreateDto>(tennisCourt);
                _response.StatusCode = HttpStatusCode.Created;
                _response.IsSuccess = true;
                return CreatedAtAction("GetTennisCourt", new { id = tennisCourt.Id }, _response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessage
                    = new List<string> { ex.ToString() };
            }
            return _response;
        }

        [HttpPut("{id:int}", Name = "UpdateTennisCourt")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> UpdateTennisCourt([FromBody] TennisCourtUpdateDto updateTennisCourtDto, int id)
        {


            try
            {
                if (updateTennisCourtDto == null || id != updateTennisCourtDto.Id)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    return BadRequest(_response);
                }

                TennisCourt tennisCourt = _mapper.Map<TennisCourt>(updateTennisCourtDto);

                await _tennisCourtRepository.UpdateAsync(tennisCourt);
                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;
                _response.Result = tennisCourt;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessage
                    = new List<string> { ex.ToString() };
            }
            return _response;
        }

        [HttpPatch("{id:int}", Name = "UpdateTennisCourtPartially")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> UpdateTennisCourtPartially(JsonPatchDocument<TennisCourtUpdateDto> patchDTO, int id)
        {
            try
            {

                if (patchDTO == null || id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    return BadRequest(_response);
                }

                var tennisCourt = await _tennisCourtRepository.GetAsync(u => u.Id == id, tracked: false);

                TennisCourtUpdateDto updateTennisCourtDto = _mapper.Map<TennisCourtUpdateDto>(tennisCourt);

                if (tennisCourt == null)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    return BadRequest(_response);
                }

                patchDTO.ApplyTo(updateTennisCourtDto, ModelState);
                TennisCourt tennisCourtModel = _mapper.Map<TennisCourt>(updateTennisCourtDto);

                await _tennisCourtRepository.UpdateAsync(tennisCourtModel);

                if (!ModelState.IsValid)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    return BadRequest(ModelState);
                }

                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessage
                    = new List<string> { ex.ToString() };
            }
            return _response;
        }


        [HttpDelete("{id:int}", Name = "DeleteTennisCourtDetail")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> DeleteTennisCourt(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    return BadRequest(_response);

                }
                var tennisCourt = await _tennisCourtRepository.GetAsync(u => u.Id == id);
                if (tennisCourt == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.IsSuccess = false;
                    return NotFound(_response);
                }

                await _tennisCourtRepository.RemoveAsync(tennisCourt);
                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessage
                    = new List<string> { ex.ToString() };
            }
            return _response;
        }
    }
}
