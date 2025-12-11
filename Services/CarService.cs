using Cars.Observers;
using CarsApi.Data;
using CarsApi.Models;
using CarsApi.Repositories;

namespace CarsApi.Services
{
    public class CarService : ICarService
    {
        private readonly ICarRepository _repo;

        public CarService(ICarRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<Car>> GetAllAsync(string? make = null, decimal? minWeight = null)
        {
            return await _repo.GetAllAsync(make, minWeight);
        }

        public async Task<Car?> GetByIdAsync(int id)
        {
            return await _repo.GetByIdAsync(id);
        }

        public async Task<Car> CreateAsync(Car car)
        {
            
            if (string.IsNullOrWhiteSpace(car.Make))
                throw new ArgumentException("Make is required");
            if (string.IsNullOrWhiteSpace(car.Model))
                throw new ArgumentException("Model is required");
            if (car.Weight < 0)
                throw new ArgumentException("Weight must be >= 0");

            await _repo.AddAsync(car);
            return car;
        }

        public async Task<bool> UpdateAsync(int id, Car car)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null) return false;

            existing.Make = car.Make;
            existing.Model = car.Model;
            existing.Year = car.Year;
            existing.Weight = car.Weight;
            existing.Unit = car.Unit;

            await _repo.UpdateAsync(existing);
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null) return false;

            await _repo.DeleteAsync(existing);
            return true;
        }
    }
    
}

