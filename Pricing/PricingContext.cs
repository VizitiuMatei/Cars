using CarsApi.Models;

namespace CarsApi.Pricing
{
    public class PricingContext
    {
        private IPricingStrategy _strategy;

        public PricingContext(IPricingStrategy strategy)
        {
            _strategy = strategy;
        }

        public void SetStrategy(IPricingStrategy strategy)
        {
            _strategy = strategy;
        }

        public decimal CalculatePrice(Car car)
        {
            if (_strategy == null)
                throw new InvalidOperationException("Pricing strategy has not been set.");

            return _strategy.CalculatePrice(car);
        }

        public string GetStrategyName()
        {
            return _strategy?.GetStrategyName() ?? "No strategy selected";
        }
    }
}
