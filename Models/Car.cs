namespace CarsApi.Models
{
    public class Car
    {
        public int Id { get; set; }             
        public string Make { get; set; } = string.Empty;   
        public string Model { get; set; } = string.Empty;  
        public int Year { get; set; }      
        public decimal Weight { get; set; }   
        public string Unit { get; set; } = "KG";
        public string FuelType { get; set; }
        public decimal BasePrice { get; set; } 
        
    }
}