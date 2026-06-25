

using Microsoft.EntityFrameworkCore.Storage;
using Veterinaria.Domain.Entities;
using Veterinaria.Infreaestructure.Data;

namespace Veterinaria.Application.Interface.Repository
{
   
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private  IDbContextTransaction? _transaction;

        public ICitaRepository Citas { get; }

        public IDuenoRepository Duenos { get; }

        public IMascotaRepository Mascotas { get; }

        public IVeterinarioRepository Veterinarios { get; }

        public UnitOfWork(ApplicationDbContext context, ICitaRepository citaRepository , IDuenoRepository duenoRepository,IMascotaRepository mascotaRepository,IVeterinarioRepository veterinarioRepository)
        {
            _context = context;
            Duenos = duenoRepository;
            Citas = citaRepository;
            Mascotas = mascotaRepository;
            Veterinarios = veterinarioRepository;

        }

        public async Task BeginTransactionAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
            await _transaction!.CommitAsync();
        }

        public async Task RollbackAsync()
        {
          await _transaction!.RollbackAsync();
        }
    }
}
