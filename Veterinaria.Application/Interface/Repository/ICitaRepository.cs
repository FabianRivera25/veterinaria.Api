
using Veterinaria.Domain.Entities;

namespace Veterinaria.Application.Interface.Repository
{
    public interface ICitaRepository
    {
        Task<Cita?> ObtenerporIdAsync(int id);
        Task<IEnumerable<Cita>> ObtenerCitasAsync(string? filtro, int pagina, int tamano);
        Task<IEnumerable<Cita>> BuscarPorFechaAsync(DateOnly FechaInicio, DateOnly FechaFin, int pagina, int tamano);
        Task<IEnumerable<Cita>> BuscarPorUsuariosync(string IdUsuario, int pagina, int tamano);
        Task<int> ContarAsync(string? filtro);
        Task<int> ContarBusquedaPorFechaAsync(DateOnly FechaInicio, DateOnly FechaFin);
        Task<int> ContarBusquedaPorUsuarioAsync(string IdUsuario);



        Task CrearAsync(Cita cita);
        //Task MarcarComoModificadoAsync(Cita cita);
        Task CambiarEstado(Cita cita);
        Task<bool> ExisteCitaEnFechaAsync(DateTime fecha, int? idAExcluir = null);
    }
}
