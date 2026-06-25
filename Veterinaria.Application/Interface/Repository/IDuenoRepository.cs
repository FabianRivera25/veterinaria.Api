
using Veterinaria.Domain.Entities;

namespace Veterinaria.Application.Interface.Repository
{
    public interface IDuenoRepository
    {
        Task<Dueno?> ObtenerporIdAsync(int id);
        Task<IEnumerable<Dueno>> ObtenerTodasAsync();
        Task<IEnumerable<Dueno>> BuscarDuenoAsync(string nombre);
        Task<bool> ExisteDniAsync(string nombre);



        Task CrearAsync(Dueno dueno);
        Task ActualizarAsync(Dueno dueno);
        Task EliminarAsync(int id);
    }
}
