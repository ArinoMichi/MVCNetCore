using Microsoft.EntityFrameworkCore;
using MvcCrudDepartamentosEF.Data;
using MvcCrudDepartamentosEF.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddTransient<RepositoryDepartamentos>();

string connectionString = builder.Configuration.GetConnectionString("SqlHospital");

builder.Services.AddDbContext<DepartamentoContext>(options => options.UseSqlServer(connectionString));

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
    pattern: "{controller=Departamentos}/{action=Index}");

app.Run();
