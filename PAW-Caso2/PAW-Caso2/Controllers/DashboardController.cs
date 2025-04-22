using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PAW_Caso2.Models;

namespace PAW_Caso2.Controllers
{
    public class DashboardController : Controller
    {
        private readonly EventCorpContext _context;

        public DashboardController(EventCorpContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var mesActual = DateTime.Now.Month;
            var anioActual = DateTime.Now.Year;

            var totalEventos = await _context.Eventos.CountAsync();
            var totalUsuarios = await _context.Usuarios.CountAsync(); // Puedes ajustar si tienes campo "Activo"
            var asistenciasMes = await _context.Asistencias
                .Where(a => a.FechaAsistencia.Month == mesActual && a.FechaAsistencia.Year == anioActual)
                .CountAsync();

            var topEventos = await _context.Eventos
                .Select(e => new TopEvento
                {
                    Titulo = e.Titulo,
                    Categoria = e.Categoria.Nombre,
                    Fecha = e.Fecha,
                    CantidadAsistentes = _context.Inscripciones.Count(i => i.EventoId == e.Id)
                })
                .OrderByDescending(e => e.CantidadAsistentes)
                .Take(5)
                .ToListAsync();

            var dashboard = new Dashboard
            {
                TotalEventos = totalEventos,
                TotalUsuariosActivos = totalUsuarios,
                AsistenciasMesActual = asistenciasMes,
                TopEventos = topEventos
            };

            return View(dashboard);
        }
    }
}
