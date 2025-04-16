using System.ComponentModel.DataAnnotations;

namespace PAW_Caso2.Models
{
    public class Asistencia
    {
        public int Id { get; set; }

        [Required]
        public int EventoId { get; set; } // Clave foránea al evento

        [Required]
        public int UsuarioId { get; set; } // Clave foránea al usuario

        [Required]
        public DateTime FechaAsistencia { get; set; } = DateTime.Now; // Fecha y hora en la que se marcó la asistencia

        [Required]
        public bool Asistio { get; set; } // true = asistió, false = no asistió

        // Relaciones de navegación
        public Evento? Evento { get; set; }
        public Usuario? Usuario { get; set; }
        public Inscripcion? Inscripcion { get; set; }
    }
}
