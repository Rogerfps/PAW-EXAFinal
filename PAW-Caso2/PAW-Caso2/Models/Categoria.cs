using System.ComponentModel.DataAnnotations;

namespace PAW_Caso2.Models
{
    public class Categoria
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Nombre { get; set; }

        [Required]
        [MaxLength(250)]
        public string Descripcion { get; set; }

        [Required]
        [MaxLength(25)]
        public string Estado { get; set; }

        [Required]
        public DateTime FechaRegistro { get; set; } = DateTime.Now;

        [Required]
        public int UsuarioRegistro { get; set; }

        public Usuario? Usuario { get; set; }

        // Relacion con Eventos
        public IEnumerable<Evento> Eventos { get; set; } = new List<Evento>();
    }
}
