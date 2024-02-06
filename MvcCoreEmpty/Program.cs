var builder = WebApplication.CreateBuilder(args);
//A�ADIR LOS SERVICIOS DE VISTAS Y CONTROLADORES
builder.Services.AddControllersWithViews();

var app = builder.Build();

//DEBEMOS INDICAR AL SERVIDOR QUE SISTEMA DE RUTAS
//UTILIZARA
//https://servidor/Controller/Vista
//app.MapGet("/", () => "Hello World!");

app.UseStaticFiles();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}");

app.Run();
