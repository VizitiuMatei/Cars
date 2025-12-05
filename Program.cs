using CarsApi.Adapters;
using CarsApi.Data;
using CarsApi.Repositories;
using CarsApi.Services;
using Microsoft.EntityFrameworkCore;
 
var builder = WebApplication.CreateBuilder(args);
 
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
if (!string.IsNullOrWhiteSpace(connectionString))
{
    builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseSqlServer(connectionString));
}
else
{
    builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseInMemoryDatabase("CarsDb"));
}
 
 
builder.Services.AddSingleton<CarsApi.Factories.CarFactory>(CarsApi.Factories.CarFactory.Instance);

builder.Services.AddHttpClient<IExternalCarApiAdapter, ExternalCarApiAdapter>();
builder.Services.AddScoped<ICarRepository, CarRepository>();
builder.Services.AddScoped<ICarService, CarService>();
 
builder.Services.AddHttpClient<IExternalCarApiAdapter, ExternalCarApiAdapter>(client =>
{
    client.BaseAddress = new Uri("https://api.carsdata.com/cars"); 
    client.Timeout = TimeSpan.FromSeconds(10);
});

var app = builder.Build();
 
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
 
app.UseHttpsRedirection();
 
app.UseAuthorization();
 
app.MapControllers();
 
app.Run();

