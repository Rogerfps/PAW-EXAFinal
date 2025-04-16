using Microsoft.EntityFrameworkCore;
using PAW_Caso2.Models; 

namespace PAW_Caso2.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Evento> Eventos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Inscripcion> Inscripciones { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
    }
}
