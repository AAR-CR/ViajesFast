using Microsoft.EntityFrameworkCore;
using API_Vuelos.Models;

namespace API_Vuelos.Data
{
    public class ViajesFastDbConext : DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Vuelo> Vuelos { get; set; }
        public DbSet<Reserva> Reservas { get; set; }

        public ViajesFastDbConext(DbContextOptions<ViajesFastDbConext> options) : base(options)
        {

        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración de relaciones
            modelBuilder.Entity<Reserva>()
                .HasOne(r => r.Usuario)
                .WithMany(u => u.Reservas)
                .HasForeignKey(r => r.UsuarioId);

            modelBuilder.Entity<Reserva>()
                .HasOne(r => r.Vuelo)
                .WithMany(v => v.Reservas)
                .HasForeignKey(r => r.VueloId);
        }


    }
}
