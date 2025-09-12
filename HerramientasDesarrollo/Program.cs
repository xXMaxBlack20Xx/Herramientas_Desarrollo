using Datos.Contexto;
using HerramientasDesarrollo.Components;
using Microsoft.EntityFrameworkCore;
using Negocios.Repositorios.PlanesDeEstudio;
using AutoMapper;

var builder = WebApplication.CreateBuilder(args);


/*==========================================================================================*/
//                          Servicios agregados para el proyecto                            //
/*==========================================================================================*/

// Contextos

// Configuración de la cadena de conexión a la base de datos
var connectionString = builder.Configuration.GetConnectionString("ContextDB") 
    ?? throw new InvalidOperationException("Connection string 'ContextDB' not found.");

builder.Services.AddDbContext<D_ContextDB>(options =>
    options.UseSqlServer(connectionString));

// Aqui se van a ir agregando las inyecciones que tiene la presentaicon
//builder.Services.AddScoped<>
builder.Services.AddScoped<CarreraNegocios>();

// Servicios
//builder.Services.AddScoped<ICarreraServicios, CarreraServicios>();

builder.Services.AddScoped<
    Datos.IRepositorios.PlanesDeEstudio.ICarreraRepositorios,
    Datos.Repositorios.PlanesDeEstudio.CarreraRepositorio
>();

builder.Services.AddScoped<
    Servicios.IRepositorios.PlanesDeEstudio.ICarreraServicios,
    Servicios.Repositorios.PlanesDeEstudio.CarreraServicios
>();


builder.Services.AddAutoMapper(cfg => { },
    typeof(Program).Assembly,
    typeof(Servicios.Repositorios.PlanesDeEstudio.CarreraServicios).Assembly,
    typeof(Negocios.Repositorios.PlanesDeEstudio.CarreraNegocios).Assembly
// agrega más assemblies si ahí viven tus Profiles
);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
