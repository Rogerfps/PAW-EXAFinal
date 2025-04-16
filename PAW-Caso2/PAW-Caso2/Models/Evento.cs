using System.ComponentModel.DataAnnotations;

namespace PAW_Caso2.Models
{
    public class Evento
    {
        public int Id { get; set; }

        [Required]
        public string Titulo { get; set; }

        [Required]
        public string Descripcion { get; set; }

        [Required]
        public int CategoriaId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Fecha { get; set; }

        [Required]
        [DataType(DataType.Time)]
        public TimeSpan Hora { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "La duración debe ser positiva.")]
        public int Duracion { get; set; } // minutos

        [Required]
        public string Ubicacion { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "El cupo debe ser mayor a 0.")]
        public int CupoMaximo { get; set; }

        public DateTime FechaRegistro { get; set; }

        public int UsuarioRegistroId { get; set; }
        public Usuario? UsuarioRegistro { get; set; }

        
        public Categoria Categoria { get; set; }

        // Relaciones
        public IEnumerable<Inscripcion>? Inscripciones { get; set; }
    }
}