namespace CarsApi.Factories
{
    using System;
    using Models; 


    public sealed class CarFactory
    {
        private static CarFactory? _instance;
        private static readonly object _lock = new object();


        private CarFactory() { }


        public static CarFactory Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                            _instance = new CarFactory();
                    }
                }
                return _instance;
            }
        }


        public Car CreateCar(string carType, string make, string model, int year, decimal basePrice = 0)
        {
            var t = carType?.Trim().ToLowerInvariant();
            return t switch
            {
                "electric" or "ev" => new ElectricCar { Make = make, Model = model, Year = year, BasePrice = basePrice, FuelType = "Electric" },
                "hybrid" or "hev" => new HybridCar { Make = make, Model = model, Year = year, BasePrice = basePrice, FuelType = "Hybrid" },
                "diesel" => new DieselCar { Make = make, Model = model, Year = year, BasePrice = basePrice, FuelType = "Diesel" },
                "petrol" or "ice" => new PetrolCar { Make = make, Model = model, Year = year, BasePrice = basePrice, FuelType = "Petrol" },
                _ => throw new ArgumentException($"Unknown car type: {carType}")
            };
        }
    }
}