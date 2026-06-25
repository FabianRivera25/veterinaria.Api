using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Veterinaria.Application.Interface.Repository;
using Veterinaria.Domain.Entities;
using Veterinaria.Infreaestructure.Data;

namespace Veterinaria.Infreaestructure.Repository
{
    public class MascotaRepository : IMascotaRepository
    {
        private readonly ApplicationDbContext _context;

        public MascotaRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task ActualizarAsync(Mascota mascota)
        {
            _context.Mascotas.Update(mascota);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Mascota>> BuscarMascotaAsync(string nombre)
        {
            var query = _context.Mascotas
               .AsNoTracking()
               .AsQueryable();

            if (!string.IsNullOrWhiteSpace(nombre))
            {
                var busqueda = nombre.Trim().ToLower();

                query = query.Where(m => m.NombreMascota.ToLower().Contains(busqueda));
            }

            return await _context.Mascotas
                .OrderBy(m => m.NombreMascota)
                .ToListAsync();
        }

        public async Task CrearAsync(Mascota mascota)
        {
            _context.Mascotas.Add(mascota);
            await _context.SaveChangesAsync();
        }

        public async Task EliminarAsync(int id)
        {
            await _context.Mascotas.Where(m => m.Id == id).ExecuteDeleteAsync();
        }

        public async Task<Mascota?> ObtenerporIdAsync(int id)
        {
            return await _context.Mascotas.Include(m => m.Dueno).FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Mascota>> ObtenerTodasAsync()
        {
            return await _context.Mascotas.Include(m => m.Dueno).ToListAsync();
        }
    }
}
