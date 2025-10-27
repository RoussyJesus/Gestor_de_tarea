using Gestor_de_tareas.Datos;
using Gestor_de_tareas.Repositorios;

var builder = WebApplication.CreateBuilder(args);


var cadena = builder.Configuration.GetConnectionString("DefaultConnection")
             ?? throw new InvalidOperationException("Falta la cadena de conexión DefaultConnection.");


builder.Services.AddSingleton(new ConexionBd(cadena));  
builder.Services.AddScoped<RepositorioUsuarios>();       
builder.Services.AddScoped<RepositorioTareas>();

builder.Services.AddRazorPages();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.MapRazorPages();  

app.Run();