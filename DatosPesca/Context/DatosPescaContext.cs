using System;
using Microsoft.EntityFrameworkCore;
using static DatosPesca.Modelos.DatosPescaModelos;

namespace DatosPesca.Context
{
    public class DatosPescaContext : DbContext
    {
        public DatosPescaContext(DbContextOptions<DatosPescaContext> options) : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Captura> Capturas { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>()
                .HasMany(u => u.Capturas)
                .WithOne(c => c.Usuario)
                .HasForeignKey(c => c.UsuarioId)
                .OnDelete(DeleteBehavior.SetNull);
            modelBuilder.Entity<Captura>()
              .Property(c => c.Zona)
              .HasConversion<string>();
            modelBuilder.Entity<Captura>()
                .Property(c => c.Oleaje)
                .HasConversion<string>();
            modelBuilder.Entity<Captura>()
                .Property(c => c.TiempoClimatico)
                .HasConversion<string>();
            modelBuilder.Entity<Captura>()
                .Property(c => c.ClaridadAgua)
                .HasConversion<string>();
            modelBuilder.Entity<Captura>()
                .Property(c => c.EstiloPesca)
                .HasConversion<string>();
            modelBuilder.Entity<Captura>()
                .Property(c => c.TipoCebo)
                .HasConversion<string>();
            modelBuilder.Entity<Captura>()
                .Property(c => c.TamañoAnzuelo)
                .HasConversion<string>();
            modelBuilder.Entity<Captura>()
                .Property(c => c.TipoSeñuelo)
                .HasConversion<string>();
            base.OnModelCreating(modelBuilder);
        }
    }
}
