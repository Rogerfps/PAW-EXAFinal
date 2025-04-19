using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using PAW_Caso2.Models;
using PAW_Caso2.Services;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authorization;



var builder = WebApplication.CreateBuilder(args);
//Registrar la política CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
        policy
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<EventCorpContext>(op =>
{
    op.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddIdentityCore<IdentityUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false; // Cambiar a 'true' si quieres que el usuario confirme su cuenta
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = true;
}).AddEntityFrameworkStores<EventCorpContext>()
.AddDefaultTokenProviders();

//Auth
builder.Services.AddScoped<AuthService>();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Auth/Login";
        options.LogoutPath = "/Auth/Logout";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
        options.SlidingExpiration = true;
    });

builder.Services.AddAuthorization();

builder.Services.AddHttpContextAccessor();

// Swagger / Minimal API explorer
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "EventCorp API",
        Version = "v1"
    });
});

var app = builder.Build();

//  Middleware
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
else
{
    // solo en desarrollo
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "EventCorp API v1");
        c.RoutePrefix = "swagger"; // acceso en /swagger
    });
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
// Aplicar CORS
app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();

// Minimal API de eventos
app.MapGet("/api/events",
    async (EventCorpContext db) => await db.Eventos.ToListAsync()
)
.AllowAnonymous();   // ? aquí ¡encadenado!

app.MapGet("/api/events/{id:int}",
    async (int id, EventCorpContext db) =>
        await db.Eventos.FindAsync(id) is Evento ev
            ? Results.Ok(ev)
            : Results.NotFound()
)
.AllowAnonymous();

 app.MapPost("/api/events",
     async (Evento ev, EventCorpContext db) =>
     {
        ev.FechaRegistro = DateTime.UtcNow;
       db.Eventos.Add(ev);
        await db.SaveChangesAsync();
       return Results.Created($"/api/events/{ev.Id}", ev);
   }
)
 .AllowAnonymous();
//  Rutas MVC
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

app.Run();