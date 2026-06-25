using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Veterinaria.Application.Interface.Repository;
using Veterinaria.Domain.Entities;
using Veterinaria.Infreaestructure.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Veterinaria.Infreaestructure.Repository
{
    public class VeterinarioRepository : IVeterinarioRepository
    {
        private readonly ApplicationDbContext _context;

        public VeterinarioRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task ActualizarAsync(Veterinario veterinario)
        {
            _context.Veterinarios.Update(veterinario);
            await _context.SaveChangesAsync();
        }

        //public async Task<IEnumerable<Veterinario>> BuscarVeterinarioaAsync(string nombre)
        //{
        //    var query = _context.Veterinarios
        //       .AsNoTracking()
        //       .AsQueryable();

        //    if (!string.IsNullOrWhiteSpace(nombre))
        //    {
        //        var busqueda = nombre.Trim().ToLower();

        //        query = query.Where(C => C.NombreVeterinario.ToLower().Contains(busqueda));
        //    }

        //    return await _context.Veterinarios
        //        .OrderBy(c => c.NombreVeterinario)
        //        .ToListAsync();
        //}

        public async Task<IEnumerable<Veterinario>> BuscarVeterinarioAsync(string nombre)
        {
            var query = _context.Veterinarios
                .AsNoTracking()
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(nombre))
            {
                var buesqueda = nombre.Trim().ToLower();

                query = query.Where(v => v.NombreVeterinario.ToLower().Contains(buesqueda));
            }

            return await _context.Veterinarios
                .OrderBy(v => v.NombreVeterinario)
                .ToListAsync();
        }

        public async Task CrearAsync(Veterinario veterinario)
        {
            _context.Veterinarios.Add(veterinario);
            await _context.SaveChangesAsync();
        }

        public async Task EliminarAsync(int id)
        {
            await _context.Veterinarios.Where(c => c.Id == id).ExecuteDeleteAsync();
        }

        public Task<bool> ExisteDniAsync(string DNI)
        {
            var dniunico = DNI.Trim().ToLower();

            return _context.Veterinarios
                .AnyAsync(c => c.DNI.Trim().ToLower() == dniunico);
        }

        public async Task<Veterinario?> ObtenerporIdAsync(int id)
        {
            return await _context.Veterinarios.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Veterinario>> ObtenerTodasAsync()
        {
            return await _context.Veterinarios.ToListAsync();
        }
    }
}
