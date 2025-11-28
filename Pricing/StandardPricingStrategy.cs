using System;
using CarsApi.Models;

namespace CarsApi.Pricing
{
    public class StandardPricingStrategy : IPricingStrategy
    {
        public decimal CalculatePrice(Car car)
        {
            var basePrice = car.BasePrice > 0 ? car.BasePrice : 20000m;
            var age = DateTime.UtcNow.Year - car.Year;
            var depreciation = Math.Pow(0.85, Math.Min(age, 20));
            var price = basePrice * (decimal)depreciation;
            if (price < 1000m) price = 1000m;
            return decimal.Round(price, 2);
        }
        public string GetStrategyName() => "Standard";
    }
}