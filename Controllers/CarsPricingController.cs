using Microsoft.AspNetCore.Mvc;
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

            var vintageCars = new List<Car>();
            var electricCars = new List<Car>();
            var luxuryCars = new List<Car>();
            var standardCars = new List<Car>();
            
                if (car.Year < 2000)
                {
                    vintageCars.Add(car);
                }
                else if (car.FuelType?.ToLower() == "electric")
                {
                    electricCars.Add(car);
                }
                else if (car.Make?.ToLower() == "audi" ||
                         car.Make?.ToLower() == "bmw" ||
                         car.Make?.ToLower() == "mercedes")
                {
                    luxuryCars.Add(car);
                }
                else
                {
                    standardCars.Add(car);
                }

                var pricingContext = new PricingContext(new VintagePricingStrategy());

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
    }
}
