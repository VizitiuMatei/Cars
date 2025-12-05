namespace CarsApi.Models
{
    public class ExternalCarDto
    {
        public string Manufacturer { get; set; } 
        public string ModelName { get; set; } 
        public int ProductionYear { get; set;  }
        public string EngineType { get; set; } 
        
        public ExternalCarDto() {} 
        
        public ExternalCarDto(string manufacturer, string modelName, int productionYear, string engineType)
        {
            Manufacturer = manufacturer;
            ModelName = modelName;
            ProductionYear = productionYear;
            EngineType = engineType;
        }
    }
}