using System;
using CarsApi.Models;
namespace CarsApi.Pricing
{
    public class VintagePricingStrategy : IPricingStrategy
    {
        public decimal CalculatePrice(Car car)
        {
            var basePrice = car.BasePrice > 0 ? car.BasePrice : 15000m;
            var age = DateTime.UtcNow.Year - car.Year;
            if (age < 25) age = 25;
            var appreciation = 1 + (age - 25) * 0.03m;
            var price = basePrice * appreciation;
            if (price < 3000m) price = 3000m;
            return decimal.Round(price, 2);
        }
        public string GetStrategyName() => "Vintage";
    }
}