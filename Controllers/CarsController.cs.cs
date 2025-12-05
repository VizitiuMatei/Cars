using CarsApi.Adapters;
using CarsApi.Models;
using CarsApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace CarsApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarsController : ControllerBase
    {
        private readonly ICarService _service;
        private readonly IExternalCarApiAdapter _externalAdapter;

        public CarsController(ICarService service, IExternalCarApiAdapter externalAdapter)
        {
            _service = service;
            _externalAdapter = externalAdapter;
        }

        
        [HttpGet]
        public async Task<ActionResult<List<Car>>> Get([FromQuery] string? make, [FromQuery] decimal? minWeight)
        {
            var cars = await _service.GetAllAsync(make, minWeight);
            return Ok(cars);
        }

        
        [HttpGet("{id}")]
        public async Task<ActionResult<Car>> Get(int id)
        {
            var car = await _service.GetByIdAsync(id);
            if (car == null) return NotFound();
            return Ok(car);
        }

        
        [HttpPost]
        public async Task<ActionResult<Car>> Post(Car car)
        {
            try
            {
                var created = await _service.CreateAsync(car);
                return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Car car)
        {
            var updated = await _service.UpdateAsync(id, car);
            if (!updated) return NotFound();
            return NoContent();
        }

        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
        [HttpGet("external")]
        public async Task<IActionResult> GetExternalCars()
        {
            try
            {
                var cars = await _externalAdapter.GetCarsAsync();
                return Ok(cars);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Failed to fetch external cars: {ex.Message}");
            }
        }
    }
}
