using Microsoft.AspNetCore.Mvc;
using CarsApi.Adapters;
using CarsApi.Models;

namespace CarsApi.Controllers
{
    [ApiController]
    [Route("api/external-cars")]
    public class ExternalCarsController : ControllerBase
    {
        private readonly IExternalCarApiAdapter _adapter;

        public ExternalCarsController(IExternalCarApiAdapter adapter)
        {
            _adapter = adapter;
        }

        [HttpGet]
        public async Task<IActionResult> GetCars()
        {
            var cars = await _adapter.GetCarsAsync();
            return Ok(cars);
        }
    }
}