using NetCoreLinqToSqlInjection.Models;
using NetCoreLinqToSqlInjection.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//builder.Services.AddSingleton<Coche>();
//builder.Services.AddTransient<Coche>();

//builder.Services.AddSingleton<ICoche, Coche>();
//builder.Services.AddSingleton<ICoche, Deportivo>();


//builder.Services.AddTransient<IRepositoryDoctores, RepositoryDoctoresSQLServer>();
builder.Services.AddTransient<IRepositoryDoctores,RepositoryDoctoresOracle>();

Coche car = new Coche
{
    Marca = "DMG",
    Modelo = "Delorean",
    Imagen = "carreras.png",
    Velocidad = 0,
    VelocidadMaxima = 260
};
//PARA PODER ENVIAR EL OBJETO SE UTILIZA LAMBDA
builder.Services.AddSingleton<ICoche, Coche>(x => car);

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
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
