using Microsoft.EntityFrameworkCore;
using PracticaComics.Data;
using PracticaComics.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//ADD CONNECTION STRING
//string connectionString = builder.Configuration.GetConnectionString("SqlHospital");
string connectionString = builder.Configuration.GetConnectionString("OracleHospital");

//ADD REPOSITORY
//builder.Services.AddTransient<IRepositoryComics, RepositoryComicsSqlServer>();
builder.Services.AddTransient<IRepositoryComics, RepositoryComicsOracle>();

//ADD CONTEXT
builder.Services.AddDbContext<HospitalContext>
//(options => options.UseSqlServer(connectionString));
(options => options.UseOracle(connectionString,
options => options.UseOracleSQLCompatibility("11")));


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
    pattern: "{controller=Comics}/{action=Index}");

app.Run();
