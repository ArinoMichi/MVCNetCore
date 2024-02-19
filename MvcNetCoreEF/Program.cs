using Microsoft.EntityFrameworkCore;
using MvcNetCoreEF.Data;
using MvcNetCoreEF.Models;
using MvcNetCoreEF.Repositories;
using static System.Runtime.InteropServices.JavaScript.JSType;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

string connectionString = builder.Configuration.GetConnectionString("SqlHospital");

//RESOLVEMOS EL REPOSITORY CON TRANSIENT
builder.Services.AddTransient<RepositoryHospital>();

//PARA INYECTAR UN CONTEXT SE UTILIZA EL SERVICE AddDbContext
//DONDE DEBEMOS INDICAR LA CADENA DE CONEXION EN SUS OPTIONS
builder.Services.AddDbContext<HospitalContext>(options => options.UseSqlServer(connectionString));


//REPOSITORIO
builder.Services.AddTransient<RepositoryHospital>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Hospitales}/{action=Index}/{id?}");

app.Run();
