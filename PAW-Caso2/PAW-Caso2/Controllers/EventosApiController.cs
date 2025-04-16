using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PAW_Caso2.Models;

namespace PAW_Caso2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventosApiController : ControllerBase
    {
        private readonly EventCorpContext _context;

        public EventosApiController(EventCorpContext context)
        {
            _context = context;
        }

        // GET: api/EventosApi
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

        // GET: api/EventosApi/5
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
