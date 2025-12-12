namespace CarsApi.Adapters
{
    using System.Net.Http;
    using System.Net.Http.Json;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Models;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Text.Json;
 
 
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
                var requestUrl = "http://localhost:5016/api/Cars";
                var response = await _httpClient.GetAsync(requestUrl);
 
                var body = response.Content == null ? string.Empty : await response.Content.ReadAsStringAsync();
                _logger.LogDebug("External API {Url} returned Status={StatusCode} Body={Body}", requestUrl, response.StatusCode, body);
 
                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogWarning("External API returned non-success status {StatusCode}", response.StatusCode);
                    return Array.Empty<Car>();
                }
 
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var resp = JsonSerializer.Deserialize<List<Car>>(body, options);
                if (resp == null) return Array.Empty<Car>();
 
                
                foreach (var car in resp)
                {
                    car.FuelType = MapEngineType(car.FuelType);
                }
 
                _logger.LogDebug("Deserialized {Count} cars from external API", resp.Count);
 
                return resp;
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