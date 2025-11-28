using CarsApi.Models;

namespace CarsApi.Adapters
{
    public interface IExternalCarApiAdapter
    {
        Task<IEnumerable<Car>> GetCarsAsync();
    }
    
}