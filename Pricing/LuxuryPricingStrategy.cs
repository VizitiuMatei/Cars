using System;
using CarsApi.Models;

namespace CarsApi.Pricing
{
    public class LuxuryPricingStrategy : IPricingStrategy
    {
        public decimal CalculatePrice(Car car)
        {
            var basePrice = car.BasePrice > 0 ? car.BasePrice : 60000m;
            var age = DateTime.UtcNow.Year - car.Year;
            var depreciation = Math.Pow(0.90, Math.Min(age, 30));
            var multiplier = 1.2m; 
            var price = basePrice * (decimal)depreciation * multiplier;
            if (price < 5000m) price = 5000m;
            return decimal.Round(price, 2);
        }
        public string GetStrategyName() => "Luxury";
    }
}