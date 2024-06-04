using Microsoft.EntityFrameworkCore;
using MvcWebMusica2.Models;
using MvcWebMusica2.ViewModels;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<GrupoBContext>(
       options => options.UseSqlServer("server=musicagrupos.database.windows.net;database=GrupoB;user=as;password=P0t@t0P0t@t0"));
builder.Services.AddScoped<IListableCancionesAlbumes, ListaCancionesAlbumes>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
