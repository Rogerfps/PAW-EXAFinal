using System.ComponentModel.DataAnnotations;

namespace PAW_Caso2.Models
{
    public class Usuario
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string NombreUsuario { get; set; }

        [Required]
        [MaxLength(100)]
        public string NombreCompleto { get; set; }

        [Required]
        [EmailAddress]
        public string Correo { get; set; }

        [Required]
        [Phone]
        public string Telefono { get; set; }

        [Required]
        [MinLength(6)]
        public string Contrasena { get; set; }

        [Required]
        public string Rol { get; set; }

        // Relacion con Eventos
        public IEnumerable<Evento>? Eventos { get; set; }
        public IEnumerable<Categoria>? Categorias { get; set; }
        public IEnumerable<Inscripcion>? Inscripciones { get; set; }
        public IEnumerable<Asistencia>? Asistencias { get; set; }

    }
}

