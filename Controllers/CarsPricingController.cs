using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarsApi.Pricing;
using CarsApi.Data;
using CarsApi.Models;


namespace CarsApi.Controllers
{
    [Route("api/cars")]
    [ApiController]
    public class CarsPricingController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CarsPricingController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("{id}/price")]
        public async Task<IActionResult> GetPrice(int id)
        {
            var car = await _context.Cars.FindAsync(id);
            if (car == null) return NotFound();

            var strategy = PricingStrategyFactory.GetStrategy(car);
            var pricingContext = new PricingContext(strategy);
            
            var response = new
            {
                car.Id,
                car.Make,
                car.Model,
                car.Year,
                car.Weight,
                car.Unit,
                car.FuelType,
                price = pricingContext.CalculatePrice(car),
                strategy = pricingContext.GetStrategyName()
            };

            return Ok(response);
        }
        [HttpGet("prices")]
        public async Task<IActionResult> GetAllPrices()
        {
            var cars = await _context.Cars.ToListAsync();
            var response = new List<object>();

            foreach (var car in cars)
            {
                var strategy = PricingStrategyFactory.GetStrategy(car);
                var context = new PricingContext(strategy);

                response.Add(new
                {
                    car.Id,
                    car.Make,
                    car.Model,
                    car.Year,
                    car.Weight,
                    car.Unit,
                    car.FuelType,
                    price = context.CalculatePrice(car),
                    strategy = context.GetStrategyName()
                });
            }

            return Ok(response);
        }
    }
}