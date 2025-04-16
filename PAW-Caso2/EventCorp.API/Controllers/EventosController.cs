using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PAW_Caso2.Models;


namespace EventCorp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventosController : ControllerBase
    {
        private readonly EventCorpContext _context;

        public EventosController(EventCorpContext context)
        {
            _context = context;
        }

        // GET: api/eventos
        [HttpGet]
        public async Task<IActionResult> GetEventos()
        {
            var eventos = await _context.Eventos
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

            return Ok(eventos);
        }

        // GET: api/eventos/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEvento(int id)
        {
            var evento = await _context.Eventos.FindAsync(id);

            if (evento == null)
                return NotFound();

            return Ok(evento);
        }
    }
}

