using LuisAngel_GabrieMorillo_AP1_P2.Models;
using Microsoft.EntityFrameworkCore;

namespace LuisAngel_GabrieMorillo_AP1_P2.DAL
{
    public class Contexto : DbContext
    {
        public Contexto(DbContextOptions<Contexto> options) : base(options) { }

        public DbSet<Entradas> Entradas { get; set; }
        public DbSet<EntradasDetalle> EntradasDetalle { get; set; }
        public DbSet<Productos> Productos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<EntradasDetalle>()
                .HasOne(d => d.Entrada)
                .WithMany(e => e.Detalles)
                .HasForeignKey(d => d.EntradaId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Productos>().HasData(
                new Productos { ProductoId = 1, Descripcion = "Maní", Peso = 0, Existencia = 100, EsCompuesto = false },
                new Productos { ProductoId = 2, Descripcion = "Pistachos", Peso = 0, Existencia = 100, EsCompuesto = false },
                new Productos { ProductoId = 3, Descripcion = "Almendras", Peso = 0, Existencia = 100, EsCompuesto = false },
                new Productos { ProductoId = 4, Descripcion = "Frutos Mixtos 200gr", Peso = 200.00, Existencia = 0, EsCompuesto = true },
                new Productos { ProductoId = 5, Descripcion = "Frutos Mixtos 400gr", Peso = 400.00, Existencia = 0, EsCompuesto = true },
                new Productos { ProductoId = 6, Descripcion = "Frutos Mixtos 600gr", Peso = 600.00, Existencia = 0, EsCompuesto = true }
            );
        }
    }
}
