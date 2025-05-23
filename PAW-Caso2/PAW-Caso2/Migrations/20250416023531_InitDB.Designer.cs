﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PAW_Caso2.Models;

#nullable disable

namespace PAW_Caso2.Migrations
{
    [DbContext(typeof(EventCorpContext))]
    [Migration("20250416023531_InitDB")]
    partial class InitDB
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.15")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("PAW_Caso2.Models.Asistencia", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("Asistio")
                        .HasColumnType("bit");

                    b.Property<DateTime>("FechaAsistencia")
                        .HasColumnType("datetime2");

                    b.Property<int>("InscripcionId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Asistencias");
                });

            modelBuilder.Entity("PAW_Caso2.Models.Categoria", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("Estado")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<DateTime>("FechaRegistro")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("UsuarioRegistroId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UsuarioRegistroId");

                    b.ToTable("Categorias");
                });

            modelBuilder.Entity("PAW_Caso2.Models.Evento", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CategoriaId")
                        .HasColumnType("int");

                    b.Property<int>("CupoMaximo")
                        .HasColumnType("int");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<int>("Duracion")
                        .HasColumnType("int");

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaRegistro")
                        .HasColumnType("datetime2");

                    b.Property<TimeSpan>("Hora")
                        .HasColumnType("time");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Ubicacion")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<int>("UsuarioRegistroId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CategoriaId");

                    b.HasIndex("UsuarioRegistroId");

                    b.ToTable("Eventos");
                });

            modelBuilder.Entity("PAW_Caso2.Models.Inscripcion", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<int>("EventoId")
                        .HasColumnType("int");

                    b.Property<DateTime>("FechaInscripcion")
                        .HasColumnType("datetime2");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EventoId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Inscripciones");
                });

            modelBuilder.Entity("PAW_Caso2.Models.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Contrasena")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Correo")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("NombreCompleto")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("NombreUsuario")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Rol")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Telefono")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.HasKey("Id");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("PAW_Caso2.Models.Categoria", b =>
                {
                    b.HasOne("PAW_Caso2.Models.Usuario", "UsuarioRegistro")
                        .WithMany("Categorias")
                        .HasForeignKey("UsuarioRegistroId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UsuarioRegistro");
                });

            modelBuilder.Entity("PAW_Caso2.Models.Evento", b =>
                {
                    b.HasOne("PAW_Caso2.Models.Categoria", "Categoria")
                        .WithMany("Eventos")
                        .HasForeignKey("CategoriaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PAW_Caso2.Models.Usuario", "UsuarioRegistro")
                        .WithMany("Eventos")
                        .HasForeignKey("UsuarioRegistroId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Categoria");

                    b.Navigation("UsuarioRegistro");
                });

            modelBuilder.Entity("PAW_Caso2.Models.Inscripcion", b =>
                {
                    b.HasOne("PAW_Caso2.Models.Evento", "Evento")
                        .WithMany("Inscripciones")
                        .HasForeignKey("EventoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PAW_Caso2.Models.Asistencia", "Asistencia")
                        .WithOne("Inscripcion")
                        .HasForeignKey("PAW_Caso2.Models.Inscripcion", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PAW_Caso2.Models.Usuario", "Usuario")
                        .WithMany("Inscripciones")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Asistencia");

                    b.Navigation("Evento");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("PAW_Caso2.Models.Asistencia", b =>
                {
                    b.Navigation("Inscripcion");
                });

            modelBuilder.Entity("PAW_Caso2.Models.Categoria", b =>
                {
                    b.Navigation("Eventos");
                });

            modelBuilder.Entity("PAW_Caso2.Models.Evento", b =>
                {
                    b.Navigation("Inscripciones");
                });

            modelBuilder.Entity("PAW_Caso2.Models.Usuario", b =>
                {
                    b.Navigation("Categorias");

                    b.Navigation("Eventos");

                    b.Navigation("Inscripciones");
                });
#pragma warning restore 612, 618
        }
    }
}
