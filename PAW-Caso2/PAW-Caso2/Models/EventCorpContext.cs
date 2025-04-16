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

            modelBuilder.Entity<Evento>(Evento =>
            { 
                Evento.HasKey(e => e.Id);
                Evento.Property(e => e.Titulo).IsRequired().HasMaxLength(100);
                Evento.Property(e => e.Descripcion).IsRequired().HasMaxLength(500);
                Evento.Property(e => e.CategoriaId).IsRequired();
                Evento.Property(e => e.Fecha).IsRequired();
                Evento.Property(e => e.Hora).IsRequired();
                Evento.Property(e => e.Duracion).IsRequired();
                Evento.Property(e => e.Ubicacion).IsRequired().HasMaxLength(200);
                Evento.Property(e => e.CupoMaximo).IsRequired();
                Evento.Property(e => e.FechaRegistro).IsRequired();
                Evento.Property(e => e.UsuarioRegistroId).IsRequired();

                Evento.HasMany(e => e.Inscripciones)
                    .WithOne(i => i.Evento)
                    .HasForeignKey(i => i.EventoId)
                    .OnDelete(DeleteBehavior.Cascade);

                Evento.HasOne(e => e.UsuarioRegistro)
                    .WithMany(u => u.Eventos)
                    .HasForeignKey(e => e.UsuarioRegistroId)
                    .OnDelete(DeleteBehavior.Restrict);

                Evento.HasOne(e => e.Categoria)
                    .WithMany(c => c.Eventos)
                    .HasForeignKey(e => e.CategoriaId)
                    .OnDelete(DeleteBehavior.Cascade);
                
            });

            modelBuilder.Entity<Inscripcion>(Inscripcion =>
            { 
                Inscripcion.HasKey(i => i.Id);
                Inscripcion.Property(i => i.EventoId).IsRequired();
                Inscripcion.Property(i => i.UsuarioId).IsRequired();
                Inscripcion.Property(i => i.FechaInscripcion).IsRequired();

                Inscripcion.HasOne(i => i.Evento)
                    .WithMany(e => e.Inscripciones)
                    .HasForeignKey(i => i.EventoId)
                    .OnDelete(DeleteBehavior.Cascade);

                Inscripcion.HasOne(i => i.Usuario)
                    .WithMany(u => u.Inscripciones)
                    .HasForeignKey(i => i.UsuarioId)
                    .OnDelete(DeleteBehavior.Restrict);

                Inscripcion.HasOne(i => i.Asistencia)
                    .WithOne(a => a.Inscripcion)
                    .HasForeignKey<Asistencia>(a => a.Id)
                    .OnDelete(DeleteBehavior.Cascade);

            });

            modelBuilder.Entity<Asistencia>(Asistencia =>
            { 
                Asistencia.HasKey(a => a.Id);
                Asistencia.Property(a => a.FechaAsistencia).IsRequired();
                Asistencia.Property(a => a.Asistio).IsRequired();
                Asistencia.Property(a => a.InscripcionId).IsRequired();

                Asistencia.HasOne(a => a.Inscripcion)
                    .WithOne(i => i.Asistencia)
                    .HasForeignKey<Inscripcion>(i => i.Id)
                    .OnDelete(DeleteBehavior.Cascade);

            });
        }
    }
}
