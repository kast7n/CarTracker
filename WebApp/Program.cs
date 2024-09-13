
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using WebApp.Data;
using WebApp.Repositories;
using WebApp.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

var connectionString = builder.Configuration.GetConnectionString("VehicleInfoManagement");
builder.Services.AddDbContext<VehicleInfoManagerContext>(options =>
{
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

builder.Services.AddTransient<IVehicleRepository, VehiclesRepository>();
builder.Services.AddTransient<IVehicleDriverRepository,VehicleDriversRepository>();
builder.Services.AddTransient<IVehicleTypeRepository,VehicleTypesRepository>();
builder.Services.AddTransient<IManufacturerRepository,ManufacturersRepository>();
builder.Services.AddTransient<IMaintenanceRepository,MaintenancesRepository>();
builder.Services.AddTransient<IDriverRepository,DriversRepository>();

var app = builder.Build();

app.UseStaticFiles();

app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=home}/{action=Index}/{id?}");



app.Run();





