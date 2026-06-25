

using Veterinaria.Application.DTOs.Usuario;
using Veterinaria.Domain.Entities;

namespace Veterinaria.Application.Interface.Service
{
    public interface IUsuarioService
    {
        Task<UsuarioDto?> ObtenerPorIdAsync(string id);
        Task<IEnumerable<UsuarioDto>> ObtenerTodosAsync(int pagina, int tamano);
        Task<int> ContarAsync();
    }
}
