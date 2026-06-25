

namespace Veterinaria.Application.Interface.Repository
{
    public interface IUnitOfWork
    {
        ICitaRepository Citas { get; }
        IDuenoRepository Duenos { get; }
        IMascotaRepository Mascotas { get; }
        IVeterinarioRepository Veterinarios { get; }

        Task BeginTransactionAsync();
        Task CommitAsync();
        Task RollbackAsync();
    }
}