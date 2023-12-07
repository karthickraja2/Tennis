using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Tennis_Court_Booking_API.Models;
using Tennis_Court_Booking_API.Models.DTO;
using Tennis_Court_Booking_API.Repository.IRepository;

namespace Tennis_Court_Booking_API.Controllers
{
    [Route("api/Bookings")]
    [ApiController]
    [Authorize]
    public class BookingsController : ControllerBase
    {

        private readonly IBookingRepository _bookingRepository;
        private readonly IMapper _mapper;
        protected APIResponse _response;
        public BookingsController(IBookingRepository bookingRepository, IMapper mapper)
        {
            _bookingRepository = bookingRepository;
            _mapper = mapper;
            _response = new APIResponse();
        }
        [HttpGet]
        [ResponseCache(CacheProfileName = "Default30")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public async Task<ActionResult<APIResponse>> GetAllBookings()
        {
            try
            {
                IEnumerable<Booking> bookingLists = await _bookingRepository.GetAllAsync();
                _response.Result = _mapper.Map<List<BookingDto>>(bookingLists); 
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

        [HttpGet("{id:int}", Name = "View booking")]
        [ResponseCache(CacheProfileName = "Default30")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> GetBooking(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.IsSuccess = false;
                    return BadRequest();
                }

                var booking = await _bookingRepository.GetAsync(u => u.Id == id);
                if (booking == null)
                {
                    _response.IsSuccess = false;
                    return NotFound();

                }
                _response.Result = _mapper.Map<BookingDto>(booking);
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

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> CreateBooking([FromBody] BookingCreateDto bookingCreateDto)
        {
            try
            {
                if (await _bookingRepository.GetAsync(u => u.UserId == bookingCreateDto.UserId) != null)
                {
                    ModelState.AddModelError("CustomError", "Booking already exists for this user");
                    _response.IsSuccess = false;
                    return BadRequest(ModelState);
                }

                if (bookingCreateDto == null)
                {
                    _response.IsSuccess = false;
                    return BadRequest(bookingCreateDto);
                }

                Booking booking = _mapper.Map<Booking>(bookingCreateDto);

                await _bookingRepository.CreateAsync(booking);

                _response.Result = _mapper.Map<Booking>(bookingCreateDto);
                _response.StatusCode = HttpStatusCode.Created;
                _response.IsSuccess = true;
                return CreatedAtAction("GetBooking", new { id = booking.Id }, _response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessage
                    = new List<string> { ex.ToString() };
            }
            return _response;
        }

        [HttpPut("{id:int}", Name = "Update Booking")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> UpdateBooking([FromBody] BookingUpdateDto bookingUpdateDto, int id)
        {


            try
            {
                if (bookingUpdateDto == null || id != bookingUpdateDto.Id)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    return BadRequest(_response);
                }

                Booking booking = _mapper.Map<Booking>(bookingUpdateDto);

                await _bookingRepository.UpdateAsync(booking);
                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;
                _response.Result = booking;
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


        [HttpDelete("{id:int}", Name = "Cancel Booking")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> DeleteBooking(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    return BadRequest(_response);

                }
                var booking = await _bookingRepository.GetAsync(u => u.Id == id);
                if (booking == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.IsSuccess = false;
                    return NotFound(_response);
                }

                await _bookingRepository.RemoveAsync(booking);
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
