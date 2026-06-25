using Microsoft.EntityFrameworkCore;
using Veterinaria.Application.Interface.Repository;
using Veterinaria.Domain.Entities;
using Veterinaria.Infreaestructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Veterinaria.Infreaestructure.Repository
{
    public class CitaRepository : ICitaRepository
    {
        private readonly ApplicationDbContext _context;

        public CitaRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Cita>> BuscarPorFechaAsync(DateOnly FechaInicio, DateOnly FechaFin, int pagina, int tamano)
        {
            var startDateTime = FechaInicio.ToDateTime(TimeOnly.MinValue);
            var endDateTime = FechaFin.ToDateTime(TimeOnly.MaxValue);

            return await _context.Citas
                .AsNoTracking()
                .Where(c => c.FechaCita >= startDateTime && c.FechaCita <= endDateTime)
                .Include(c => c.Detalles)
                .Include(c => c.Dueno)
                .Include(c => c.Veterinario)
                .OrderByDescending(c => c.FechaCita)
                .Skip((pagina - 1) * tamano)
                .Take(tamano)
                .ToListAsync();
        }

        public async Task<IEnumerable<Cita>> BuscarPorUsuariosync(string IdUsuario, int pagina, int tamano)
        {
            return await _context.Citas
                .AsNoTracking()
                .Where(c => c.IdUsuario == IdUsuario)
                .Include(c => c.Detalles)
                .Include(c => c.Dueno)
                .Include(c => c.Veterinario)
                .OrderByDescending(c => c.FechaCita)
                .Skip((pagina - 1) * tamano)
                .Take(tamano)
                .ToListAsync();
        }

        public Task CambiarEstado(Cita cita)
        {
            _context.Citas.Update(cita);
            return Task.CompletedTask;
        }

        public async Task<int> ContarAsync(string? filtro)
        {
            if (string.IsNullOrWhiteSpace(filtro))
            {
                return await _context.Citas.CountAsync();
            }

            var lowerFiltro = filtro.ToLower();
            return await _context.Citas
                .Where(c => c.Motivo.ToLower().Contains(lowerFiltro) ||
                            c.Dueno.NombreDueno.ToLower().Contains(lowerFiltro) ||
                            c.Veterinario.NombreVeterinario.ToLower().Contains(lowerFiltro) ||
                            c.Estado.ToLower().Contains(lowerFiltro) ||
                            c.Detalles.Any(d => d.NombreMascota.ToLower().Contains(lowerFiltro)))
                .CountAsync();
        }

        public async Task<int> ContarBusquedaPorFechaAsync(DateOnly FechaInicio, DateOnly FechaFin)
        {
            var startDateTime = FechaInicio.ToDateTime(TimeOnly.MinValue);
            var endDateTime = FechaFin.ToDateTime(TimeOnly.MaxValue);

            return await _context.Citas
                .Where(c => c.FechaCita >= startDateTime && c.FechaCita <= endDateTime)
                .CountAsync();
        }

        public async Task<int> ContarBusquedaPorUsuarioAsync(string IdUsuario)
        {
            return await _context.Citas
                .Where(c => c.IdUsuario == IdUsuario)
                .CountAsync();
        }

        public async Task CrearAsync(Cita cita)
        {
            await _context.Citas.AddAsync(cita);
        }

        public async Task<IEnumerable<Cita>> ObtenerCitasAsync(string? filtro, int pagina, int tamano)
        {
            IQueryable<Cita> query = _context.Citas
                .Include(c => c.Detalles)
                .Include(c => c.Dueno)
                .Include(c => c.Veterinario);

            if (!string.IsNullOrWhiteSpace(filtro))
            {
                var lowerFiltro = filtro.ToLower();
                query = query.Where(c => c.Motivo.ToLower().Contains(lowerFiltro) ||
                                         c.Dueno.NombreDueno.ToLower().Contains(lowerFiltro) ||
                                         c.Veterinario.NombreVeterinario.ToLower().Contains(lowerFiltro) ||
                                         c.Estado.ToLower().Contains(lowerFiltro) ||
                                         c.Detalles.Any(d => d.NombreMascota.ToLower().Contains(lowerFiltro)));
            }

            return await query
                .OrderBy(c => c.Estado == "Activo" ? 0 : 1)
                .ThenByDescending(c => c.FechaCita)
                .Skip((pagina - 1) * tamano)
                .Take(tamano)
                .ToListAsync();
        }

        public async Task<Cita?> ObtenerporIdAsync(int id)
        {
            return await _context.Citas
                .Include(c => c.Detalles)
                .Include(c => c.Dueno)
                .Include(c => c.Veterinario)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<bool> ExisteCitaEnFechaAsync(DateTime fecha, int? idAExcluir = null)
        {
            return await _context.Citas
                .AnyAsync(c => c.FechaCita == fecha && c.Estado == "Activo" && c.Id != idAExcluir);
        }
    }
}
