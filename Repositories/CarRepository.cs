using CarsApi.Data;
using CarsApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CarsApi.Repositories
{
    public class CarRepository : ICarRepository
    {
        private readonly AppDbContext _context;

        public CarRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Car>> GetAllAsync(string? make = null, decimal? minWeight = null)
        {
            var query = _context.Cars.AsQueryable();

            if (!string.IsNullOrEmpty(make))
                query = query.Where(c => c.Make == make);

            if (minWeight.HasValue)
                query = query.Where(c => c.Weight >= minWeight.Value);

            return await query.ToListAsync();
        }

        public async Task<Car?> GetByIdAsync(int id)
        {
            return await _context.Cars.FindAsync(id);
        }

        public async Task AddAsync(Car car)
        {
            _context.Cars.Add(car);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Car car)
        {
            _context.Cars.Update(car);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Car car)
        {
            _context.Cars.Remove(car);
            await _context.SaveChangesAsync();
        }
    }
}
