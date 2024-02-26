using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore;
using MvcCoreEFMultiplesBBDD.Data;
using MvcCoreEFMultiplesBBDD.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//CONEXIONES ORACLE O SQL
//string connectionString = builder.Configuration.GetConnectionString("SqlHospital");
//string connectionString = builder.Configuration.GetConnectionString("OracleHospital
string connectionString = builder.Configuration.GetConnectionString("MySqlHospital");


//REPOSITORIOS ORACLE O SQL

//builder.Services.AddDbContext<HospitalContext>
//(options => options.UseSqlServer(connectionString));
//(options => options.UseOracle(connectionString,
//options => options.UseOracleSQLCompatibility("11")));
//MYSQL

builder.Services.AddDbContextPool<HospitalContext>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));



//builder.Services.AddTransient<IRepositoryEmpleados,RepositoryEmpleadosSqlServer>();
builder.Services.AddTransient<IRepositoryEmpleados, RepositoryEmpleadosOracle>();
builder.Services.AddTransient<IRepositoryEmpleados,RepositoryEmpleadosMySql>();


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
