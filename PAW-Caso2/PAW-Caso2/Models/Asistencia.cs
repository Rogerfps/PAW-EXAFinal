using System.ComponentModel.DataAnnotations;

namespace PAW_Caso2.Models
{
    public class Asistencia
    {
        public int Id { get; set; }

        [Required]
        public int InscripcionId { get; set; }

        [Required]
        public DateTime FechaAsistencia { get; set; } = DateTime.Now;

        [Required]
        public bool Asistio { get; set; } = false;

        public Inscripcion? Inscripcion { get; set; }
    }
}
