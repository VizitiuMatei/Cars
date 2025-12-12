using CarsApi.Models;

namespace CarsApi.Pricing
{


    public interface IPricingStrategy
    {
        decimal CalculatePrice(Car car);
        string GetStrategyName();
    }
}