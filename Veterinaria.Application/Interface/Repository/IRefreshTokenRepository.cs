
using Veterinaria.Domain.Entities;

namespace Veterinaria.Application.Interface.Repository
{
    public interface IRefreshTokenRepository
    {
        Task GuardarAsycn(RefreshToken token);
        Task<RefreshToken?> ObtenerAsycn(string token);
        Task ActualizarAsync(RefreshToken token);
    }
}
