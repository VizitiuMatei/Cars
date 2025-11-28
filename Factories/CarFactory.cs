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


        public Car CreateCar(string carType)
        {

            var t = carType?.Trim().ToLowerInvariant();
            return t switch
            {
                "electric" => new Car { FuelType = "Electric", /* alte proprietăți default */ },
                "ev" => new Car { FuelType = "Electric" },
                "hybrid" => new Car { FuelType = "Hybrid" },
                "hev" => new Car { FuelType = "Hybrid" },
                "diesel" => new Car { FuelType = "Diesel" },
                "petrol" => new Car { FuelType = "Petrol" },
                "ice" => new Car { FuelType = "Petrol" },
                _ => throw new ArgumentException($"Unknown car type: {carType}")
            };
        }
    }
}