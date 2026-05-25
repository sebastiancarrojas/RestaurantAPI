using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RestaurantAPI.API.DTOs.Request;
using RestaurantAPI.API.DTOs.Response;
using RestaurantAPI.Domain.Entities;
using RestaurantAPI.Domain.Interfaces.Services;

namespace RestaurantAPI.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RestaurantsController : ControllerBase
    {
        private readonly IRestaurantService _restaurantService;
        private readonly IMapper _mapper;

        public RestaurantsController(IRestaurantService restaurantService, IMapper mapper)
        {
            _restaurantService = restaurantService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RestaurantResponseDTO>>> GetAll()
        {
            var restaurants = await _restaurantService.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<RestaurantResponseDTO>>(restaurants));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RestaurantResponseDTO>> GetById(int id)
        {
            var restaurant = await _restaurantService.GetByIdAsync(id);
            if (restaurant == null)
                return NotFound(new { message = $"Restaurante con ID {id} no encontrado" });
            return Ok(_mapper.Map<RestaurantResponseDTO>(restaurant));
        }

        [HttpGet("search/name")]
        public async Task<ActionResult<RestaurantResponseDTO>> GetByName([FromQuery] string name)
        {
            var restaurant = await _restaurantService.GetByNameAsync(name);
            if (restaurant == null)
                return NotFound(new { message = $"No se encontró restaurante con nombre '{name}'" });
            return Ok(_mapper.Map<RestaurantResponseDTO>(restaurant));
        }

        [HttpGet("search/city")]
        public async Task<ActionResult<IEnumerable<RestaurantResponseDTO>>> GetByCity([FromQuery] string city)
        {
            var restaurants = await _restaurantService.GetByCityAsync(city);
            return Ok(_mapper.Map<IEnumerable<RestaurantResponseDTO>>(restaurants));
        }

        [HttpPost]
        public async Task<ActionResult<RestaurantResponseDTO>> Create(RestaurantRequestDTO dto)
        {
            try
            {
                var restaurant = _mapper.Map<Restaurant>(dto);
                var created = await _restaurantService.CreateAsync(restaurant);
                var response = _mapper.Map<RestaurantResponseDTO>(created);
                return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, RestaurantRequestDTO dto)
        {
            try
            {
                var restaurant = _mapper.Map<Restaurant>(dto);
                await _restaurantService.UpdateAsync(id, restaurant);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _restaurantService.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }
}
