using CarsApi.Models;

namespace CarsApi.Services
{
    public interface ICarService
    {
        Task<List<Car>> GetAllAsync(string? make = null, decimal? minWeight = null);
        Task<Car?> GetByIdAsync(int id);
        Task<Car> CreateAsync(Car car);
        Task<bool> UpdateAsync(int id, Car car);
        Task<bool> DeleteAsync(int id);
    }
}
