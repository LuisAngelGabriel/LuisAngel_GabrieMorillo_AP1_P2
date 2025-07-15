using LuisAngel_GabrieMorillo_AP1_P2.DAL;
using LuisAngel_GabrieMorillo_AP1_P2.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace LuisAngel_GabrieMorillo_AP1_P2.Services;

    public class EntradasService(IDbContextFactory<Contexto> DbFactory)
    {
        public async Task<bool> Existe(int entradaId)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.Entradas.AnyAsync(e => e.EntradaId == entradaId);
        }

        public async Task<bool> Modificar(Entradas entrada)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();

            contexto.Entry(entrada).State = EntityState.Modified;

            var detallesExistentesEnDb = await contexto.EntradasDetalle
                .Where(d => d.EntradaId == entrada.EntradaId)
                .AsNoTracking()
                .ToListAsync();

            foreach (var detalleExistente in detallesExistentesEnDb)
            {
                if (!entrada.Detalles.Any(d => d.Id == detalleExistente.Id))
                {
                    contexto.Entry(detalleExistente).State = EntityState.Deleted;
                }
            }

            foreach (var detalle in entrada.Detalles)
            {
                if (detalle.Producto != null && detalle.Producto.ProductoId > 0)
                    contexto.Entry(detalle.Producto).State = EntityState.Unchanged;

                if (entrada.Producido != null && entrada.Producido.ProductoId > 0)
                    contexto.Entry(entrada.Producido).State = EntityState.Unchanged;

                if (detalle.Id == 0)
                {
                    detalle.EntradaId = entrada.EntradaId;
                    contexto.EntradasDetalle.Add(detalle);
                }
                else
                {
                    var existingEntry = contexto.ChangeTracker.Entries<EntradasDetalle>()
                        .FirstOrDefault(e => e.Entity.Id == detalle.Id);

                    if (existingEntry == null)
                    {
                        contexto.EntradasDetalle.Attach(detalle);
                        contexto.Entry(detalle).State = EntityState.Modified;
                    }
                    else
                    {
                        existingEntry.CurrentValues.SetValues(detalle);
                        existingEntry.State = EntityState.Modified;
                    }
                }
            }

            return await contexto.SaveChangesAsync() > 0;
        }

        public async Task<bool> Insertar(Entradas entrada)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();

            foreach (var detalle in entrada.Detalles)
            {
                if (detalle.Producto != null && detalle.Producto.ProductoId > 0)
                    contexto.Entry(detalle.Producto).State = EntityState.Unchanged;
            }

            if (entrada.Producido != null && entrada.Producido.ProductoId > 0)
                contexto.Entry(entrada.Producido).State = EntityState.Unchanged;

            contexto.Entradas.Add(entrada);
            return await contexto.SaveChangesAsync() > 0;
        }

        public async Task<bool> Guardar(Entradas entrada)
        {
            if (entrada.EntradaId == 0)
                return await Insertar(entrada);
            else
                return await Modificar(entrada);
        }

        public async Task<Entradas?> Buscar(int entradaId)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.Entradas
                .Include(e => e.Producido)
                .Include(e => e.Detalles)
                    .ThenInclude(d => d.Producto)
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.EntradaId == entradaId);
        }

        public async Task<bool> Eliminar(int entradaId)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.Entradas
                .Where(e => e.EntradaId == entradaId)
                .ExecuteDeleteAsync() > 0;
        }

        public async Task<List<Entradas>> Listar(Expression<Func<Entradas, bool>> criterio)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.Entradas
                .Include(e => e.Producido)
                .Include(e => e.Detalles)
                    .ThenInclude(d => d.Producto)
                .Where(criterio)
                .AsNoTracking()
                .ToListAsync();
        }
    }

