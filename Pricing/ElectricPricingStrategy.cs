using System;
using CarsApi.Models;
namespace CarsApi.Pricing
{
    public class ElectricPricingStrategy : IPricingStrategy
    {
        public decimal CalculatePrice(Car car)
        {
            var basePrice = car.BasePrice > 0 ? car.BasePrice : 35000m;
            var age = DateTime.UtcNow.Year - car.Year;
            var depreciation = Math.Pow(0.92, Math.Min(age, 20));
            var price = basePrice * (decimal)depreciation;
            if (price < 2000m) price = 2000m;
            return decimal.Round(price, 2);
        }
        public string GetStrategyName() => "Electric";
    }
}