using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Veterinaria.Application.Interface.Repository;
using Veterinaria.Domain.Entities;
using Veterinaria.Infreaestructure.Data;

namespace Veterinaria.Infreaestructure.Repository
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly ApplicationDbContext _context;

        public RefreshTokenRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task ActualizarAsync(RefreshToken token)
        {
            _context.RefreshTokens.Update(token);
            await _context.SaveChangesAsync();
        }

        public async Task GuardarAsycn(RefreshToken token)
        {
            _context.RefreshTokens.Update(token);
            await _context.SaveChangesAsync();
        }

        public async Task<RefreshToken?> ObtenerAsycn(string token)
        {
            return await _context.RefreshTokens
                 .Include(x => x.Usuario)
                 .FirstOrDefaultAsync(x => x.Token == token);
        }
    }
}
