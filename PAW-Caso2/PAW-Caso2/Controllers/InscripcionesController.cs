using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PAW_Caso2.Models;

public class InscripcionesController : Controller
{
    private readonly EventCorpContext _context;

    public InscripcionesController(EventCorpContext context)
    {
        _context = context;
    }

    // Vista de eventos disponibles para inscripción
    public async Task<IActionResult> EventosDisponibles(int usuarioId)
    {
        var eventosDisponibles = await _context.Eventos
            .Where(e => !_context.Inscripciones.Any(i => i.UsuarioId == usuarioId && i.EventoId == e.Id))
            .ToListAsync();

        return View(eventosDisponibles);
    }

    // Inscripción con validaciones
    [HttpPost]
    public async Task<IActionResult> Inscribirse(int eventoId, int usuarioId)
    {
        var evento = await _context.Eventos.FindAsync(eventoId);
        if (evento == null)
            return NotFound();

        // Validar superposición de horarios
        var inscripcionesUsuario = await _context.Inscripciones
            .Include(i => i.Evento)
            .Where(i => i.UsuarioId == usuarioId)
            .ToListAsync();

        foreach (var insc in inscripcionesUsuario)
        {
            var e = insc.Evento;
            if (e.Fecha.Date == evento.Fecha.Date &&
                evento.Hora < e.Hora.Add(TimeSpan.FromMinutes(e.Duracion)) &&
                evento.Hora.Add(TimeSpan.FromMinutes(evento.Duracion)) > e.Hora)
            {
                TempData["Error"] = "Ya estás inscrito en un evento que se superpone en horario.";
                return RedirectToAction("EventosDisponibles", new { usuarioId });
            }
        }

        // Validar cupo
        var totalInscritos = await _context.Inscripciones.CountAsync(i => i.EventoId == eventoId);
        if (totalInscritos >= evento.CupoMaximo)
        {
            TempData["Error"] = "El evento ya alcanzó su cupo máximo.";
            return RedirectToAction("EventosDisponibles", new { usuarioId });
        }

        // Crear inscripción
        var nuevaInscripcion = new Inscripcion
        {
            EventoId = eventoId,
            UsuarioId = usuarioId,
            FechaInscripcion = DateTime.Now
        };

        _context.Inscripciones.Add(nuevaInscripcion);
        await _context.SaveChangesAsync();

        TempData["Success"] = "Inscripción realizada correctamente.";
        return RedirectToAction("EventosDisponibles", new { usuarioId });
    }

    // Vista para organizadores: ver inscritos por eventos propios
    public async Task<IActionResult> InscritosPorOrganizador(int organizadorId)
    {
        var eventos = await _context.Eventos
            .Where(e => e.UsuarioRegistroId == organizadorId)
            .Include(e => e.Inscripciones)
                .ThenInclude(i => i.Usuario)
            .ToListAsync();

        return View("ListadoPorRol", eventos);
    }

    // Vista para administradores: ver todos los inscritos
    public async Task<IActionResult> InscritosPorAdministrador()
    {
        var eventos = await _context.Eventos
            .Include(e => e.Inscripciones)
                .ThenInclude(i => i.Usuario)
            .ToListAsync();

        return View("ListadoPorRol", eventos);
    }
}
