using CarsApi.Models;
using System;

namespace CarsApi.Pricing
{
    public static class PricingStrategyFactory
    {
        public static IPricingStrategy GetStrategy(Car car)
        {
            if (car.Year <= DateTime.UtcNow.Year - 25)
                return new VintagePricingStrategy();

            if (car.FuelType?.ToLower() == "electric")
                return new ElectricPricingStrategy();

            if (car.Make?.ToLower() is "bmw" or "audi" or "mercedes" or "porsche")
                return new LuxuryPricingStrategy();

            return new StandardPricingStrategy();
        }
    }
}