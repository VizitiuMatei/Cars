namespace CarsApi.Adapters
{
    using System.Net.Http;
    using System.Net.Http.Json;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Models;
    using Microsoft.Extensions.Logging;
    using System;


    public class ExternalCarApiAdapter : IExternalCarApiAdapter
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ExternalCarApiAdapter> _logger;


        public ExternalCarApiAdapter(HttpClient httpClient, ILogger<ExternalCarApiAdapter> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }


        public async Task<IEnumerable<Car>> GetCarsAsync()
        {
            try
            {
                var resp = await _httpClient.GetFromJsonAsync<List<ExternalCarDto>>("/cars");
                if (resp == null) return Array.Empty<Car>();


                var list = new List<Car>();
                foreach (var e in resp)
                {
                    var car = new Car
                    {
                        Make = e.Manufacturer,
                        Model = e.ModelName,
                        Year = e.ProductionYear,
                        FuelType = MapEngineType(e.EngineType)
                    };
                    list.Add(car);
                }
                return list;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to fetch external cars");
                throw; 
            }
        }


        private string MapEngineType(string e)
        {
            return e?.Trim().ToLowerInvariant() switch
            {
                "ev" => "Electric",
                "ice" => "Petrol",
                "hev" => "Hybrid",
                "diesel" => "Diesel",
                _ => "Unknown"
            };
        }
    }
}