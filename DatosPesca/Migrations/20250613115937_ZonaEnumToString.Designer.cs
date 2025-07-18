﻿// <auto-generated />
using System;
using DatosPesca.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DatosPesca.Migrations
{
    [DbContext(typeof(DatosPescaContext))]
    [Migration("20250613115937_ZonaEnumToString")]
    partial class ZonaEnumToString
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DatosPesca.Modelos.DatosPescaModelos+Captura", b =>
                {
                    b.Property<int>("CapturaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CapturaId"));

                    b.Property<bool>("Anzuelo")
                        .HasColumnType("bit");

                    b.Property<string>("ClaridadAgua")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EstiloPesca")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("Fecha")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Gusano")
                        .HasColumnType("bit");

                    b.Property<int>("HoraAproximada")
                        .HasColumnType("int");

                    b.Property<string>("Localidad")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NombreEspecie")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Oleaje")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Profundidad")
                        .HasColumnType("int");

                    b.Property<bool>("Señuelo")
                        .HasColumnType("bit");

                    b.Property<double>("Tamaño")
                        .HasColumnType("float");

                    b.Property<int?>("TamañoAnzuelo")
                        .HasColumnType("int");

                    b.Property<double?>("TamañoBajo")
                        .HasColumnType("float");

                    b.Property<double>("TamañoHilo")
                        .HasColumnType("float");

                    b.Property<string>("TiempoClimatico")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TipoGusano")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TipoSeñuelo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UsuarioId")
                        .HasColumnType("int");

                    b.Property<string>("Zona")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CapturaId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Capturas");
                });

            modelBuilder.Entity("DatosPesca.Modelos.DatosPescaModelos+Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("DatosPesca.Modelos.DatosPescaModelos+Captura", b =>
                {
                    b.HasOne("DatosPesca.Modelos.DatosPescaModelos+Usuario", "Usuario")
                        .WithMany("Capturas")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("DatosPesca.Modelos.DatosPescaModelos+Usuario", b =>
                {
                    b.Navigation("Capturas");
                });
#pragma warning restore 612, 618
        }
    }
}
