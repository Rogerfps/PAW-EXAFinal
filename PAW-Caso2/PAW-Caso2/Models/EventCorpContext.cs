using Microsoft.EntityFrameworkCore;

namespace PAW_Caso2.Models
{
    public class EventCorpContext : DbContext
    {
        public EventCorpContext(DbContextOptions<EventCorpContext> options) : base(options)
        {
        }

        // Tablas o Entidades de la DB
        public DbSet<Evento> Eventos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Inscripcion> Inscripciones { get; set; }
        public DbSet<Asistencia> Asistencias { get; set; }

        // Configuracion de la DB
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>(Usuario =>
            {
                Usuario.HasKey(u => u.Id);
                Usuario.Property(u => u.NombreUsuario).IsRequired().HasMaxLength(50);
                Usuario.Property(u => u.NombreCompleto).IsRequired().HasMaxLength(100);
                Usuario.Property(u => u.Correo).IsRequired().HasMaxLength(100);
                Usuario.Property(u => u.Telefono).IsRequired().HasMaxLength(15);
                Usuario.Property(u => u.Contrasena).IsRequired();
                Usuario.Property(u => u.Rol).IsRequired();

                Usuario.HasMany(u => u.Eventos)
                    .WithOne(e => e.UsuarioRegistro)
                    .HasForeignKey(e => e.UsuarioRegistroId)
                    .OnDelete(DeleteBehavior.Cascade);

                Usuario.HasMany(u => u.Categorias)
                    .WithOne(c => c.UsuarioRegistro)
                    .HasForeignKey(c => c.UsuarioRegistroId)
                    .OnDelete(DeleteBehavior.Cascade);

                Usuario.HasMany(u => u.Inscripciones)
                    .WithOne(i => i.Usuario)
                    .HasForeignKey(i => i.UsuarioId)
                    .OnDelete(DeleteBehavior.Cascade);

                Usuario.HasMany(u => u.Asistencias)
                    .WithOne(a => a.Usuario)
                    .HasForeignKey(a => a.UsuarioId)
                    .OnDelete(DeleteBehavior.Cascade);

            });

            modelBuilder.Entity<Categoria>(Categoria =>
            {
                Categoria.HasKey(c => c.Id);
                Categoria.Property(c => c.Nombre).IsRequired().HasMaxLength(50);
                Categoria.Property(c => c.Descripcion).IsRequired().HasMaxLength(250);
                Categoria.Property(c => c.Estado).IsRequired().HasMaxLength(25);
                Categoria.Property(c => c.FechaRegistro).IsRequired();
                Categoria.Property(c => c.UsuarioRegistroId).IsRequired();
                
                Categoria.HasMany(c => c.Eventos)
                    .WithOne(e => e.Categoria)
                    .HasForeignKey(e => e.CategoriaId)
                    .OnDelete(DeleteBehavior.Cascade);

                Categoria.HasOne(c => c.UsuarioRegistro)
                    .WithMany(u => u.Categorias)
                    .HasForeignKey(c => c.UsuarioRegistroId)
                    .OnDelete(DeleteBehavior.Cascade);

            });
        }
    }
}
