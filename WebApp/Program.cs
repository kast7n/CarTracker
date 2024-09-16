
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using WebApp.Data;
using WebApp.Repositories;
using WebApp.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Manager", p => p.RequireClaim("Position", "Manager"));
    options.AddPolicy("Worker", p => p.RequireClaim("Position", "Worker"));
});

var connectionString = builder.Configuration.GetConnectionString("VehicleInfoManagement");
builder.Services.AddDbContext<VehicleInfoManagerContext>(options =>
{
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

builder.Services.AddDbContext<AccountContext>(options =>
        options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));


builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<AccountContext>();



builder.Services.AddTransient<IVehicleRepository, VehiclesRepository>();
builder.Services.AddTransient<IVehicleDriverRepository,VehicleDriversRepository>();
builder.Services.AddTransient<IVehicleTypeRepository,VehicleTypesRepository>();
builder.Services.AddTransient<IManufacturerRepository,ManufacturersRepository>();
builder.Services.AddTransient<IMaintenanceRepository,MaintenancesRepository>();
builder.Services.AddTransient<IDriverRepository,DriversRepository>();

var app = builder.Build();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=home}/{action=Index}/{id?}");



app.Run();





