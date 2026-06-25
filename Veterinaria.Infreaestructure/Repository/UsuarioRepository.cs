
using Microsoft.EntityFrameworkCore;
using Veterinaria.Application.Interface.Repository;
using Veterinaria.Domain.Entities;
using Veterinaria.Infreaestructure.Data;

namespace Veterinaria.Infreaestructure.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly ApplicationDbContext _context;
        
        public UsuarioRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> ContarAsync()
        {
            return await _context.Users.CountAsync();
        }

        public async Task<ApplicationUser?> ObtenerPorIdAsync(string id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<IEnumerable<ApplicationUser>> ObtenerTodosAsync(int pagina, int tamano)
        {
            return await _context.Users
                .AsNoTracking()
                .OrderBy(u => u.NombreCompleto)
                .Skip((pagina - 1) * tamano)
                .Take(tamano)
                .ToListAsync();
        }
    }
}
