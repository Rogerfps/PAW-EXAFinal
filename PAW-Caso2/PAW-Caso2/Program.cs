using PAW_Caso2.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

var app = builder.Build();

app.MapGet("/api/events", async (ApplicationDbContext db) =>
{
    var eventos = await db.Eventos
        .Where(e => e.Fecha >= DateTime.Now)
        .Select(e => new {
            e.Id,
            e.Titulo,
            e.Fecha,
            e.Hora,
            e.Ubicacion,
            e.CupoMaximo
        })
        .ToListAsync();

    return Results.Ok(eventos);
});

app.MapGet("/api/events/{id}", async (int id, ApplicationDbContext db) =>
{
    var evento = await db.Eventos.FindAsync(id);

    if (evento == null)
        return Results.NotFound();

    return Results.Ok(evento);
});

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
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
