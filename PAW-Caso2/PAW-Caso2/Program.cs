using Microsoft.EntityFrameworkCore;
using PAW_Caso2.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<EventCorpContext>(op =>
{
    op.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
else
{
    app.UseSwagger();
    app.UseSwaggerUI();    
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

    app.MapGet("/api/events", async (EventCorpContext db) =>
    {
        var eventos = await db.Eventos
            .Where(e => e.Fecha >= DateTime.Now)
            .Select(e => new
            {
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

    app.MapGet("/api/events/{id}", async (int id, EventCorpContext db) =>
    {
        var evento = await db.Eventos.FindAsync(id);

        if (evento == null)
            return Results.NotFound();

        return Results.Ok(evento);
    });


app.Run();
