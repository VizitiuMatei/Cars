namespace CarsApi.Models
{
    public class ExternalCarDto
    {
        public string Manufacturer { get; set; } = string.Empty;
        public string ModelName { get; set; } = string.Empty;
        public int ProductionYear { get; set; }
        public string EngineType { get; set; } = string.Empty;
    }
}
