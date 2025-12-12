using Microsoft.EntityFrameworkCore;
using CarsApi.Models;

namespace CarsApi.Data
{
    public class AppDbContext : DbContext
    {
        
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Car> Cars { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=CarsDb;Trusted_Connection=True;TrustServerCertificate=True");
        }
    }

}    
