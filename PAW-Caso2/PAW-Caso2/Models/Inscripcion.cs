using System.ComponentModel.DataAnnotations;

namespace PAW_Caso2.Models
{
    public class Inscripcion
    {
        public int Id { get; set; }

        [Required]
        public int EventoId { get; set; }

        [Required]
        public string UsuarioId { get; set; }

        public DateTime FechaInscripcion { get; set; }

        // Relaciones
        public Evento? Evento { get; set; }
        public Usuario? Usuario { get; set; }

        public Asistencia? Asistencia { get; set; }
    }
}