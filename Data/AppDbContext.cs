using Microsoft.EntityFrameworkCore;
using CarsApi.Models;

namespace CarsApi.Data
{
    public class AppDbContext : DbContext
    {
        
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Car> Cars { get; set; }
    }
}