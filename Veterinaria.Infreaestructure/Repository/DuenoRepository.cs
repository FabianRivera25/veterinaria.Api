using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Veterinaria.Application.Interface.Repository;
using Veterinaria.Domain.Entities;
using Veterinaria.Infreaestructure.Data;

namespace Veterinaria.Infreaestructure.Repository
{
    public class DuenoRepository : IDuenoRepository
    {
        private readonly ApplicationDbContext _context;

        public DuenoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task ActualizarAsync(Dueno dueno)
        { 
            _context.Duenos.Update(dueno);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Dueno>> BuscarDuenoAsync(string nombre)
        {
            var query = _context.Duenos
              .AsNoTracking()
              .AsQueryable();

            if (!string.IsNullOrWhiteSpace(nombre))
            {
                var busqueda = nombre.Trim().ToLower();

                query = query.Where(C => C.NombreDueno.ToLower().Contains(busqueda));
            }

            return await _context.Duenos
                .OrderBy(c => c.NombreDueno)
                .ToListAsync();
        }

        public async Task CrearAsync(Dueno dueno)
        {
            _context.Duenos.Add(dueno);
            await _context.SaveChangesAsync();
        }

        public async Task EliminarAsync(int id)
        {
            await _context.Duenos.Where(c => c.Id == id).ExecuteDeleteAsync();
        }

        public Task<bool> ExisteDniAsync(string DNI)
        {
            var dniunico = DNI.Trim().ToLower();

            return _context.Duenos
                .AnyAsync(d => d.DNI.Trim().ToLower() == dniunico);
        }

        public async Task<Dueno?> ObtenerporIdAsync(int id)
        {
            return await _context.Duenos.Include(d => d.Usuario).FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Dueno>> ObtenerTodasAsync()
        {
            return await _context.Duenos.Include(U => U.Usuario).ToListAsync();
        }
    }
}
