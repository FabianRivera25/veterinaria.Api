using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Veterinaria.Application.DTOs.Cita;

namespace Veterinaria.Application.Interface.Service
{
    public interface ICitaService
    {
        Task<CitaDto> ObtenerporIdAsync(int id);
        Task<IEnumerable<CitaDto>> ObtenerCitasAsync(string? filtro, int pagina, int tamano);
        Task<IEnumerable<CitaDto>> BuscarPorFechaAsync(DateOnly FechaInicio, DateOnly FechaFin, int pagina, int tamano);
        Task<IEnumerable<CitaDto>> BuscarPorUsuarioAsync(string IdUsuario, int pagina, int tamano);
        Task<int> ContarAsync(string? filtro);
        Task<int> ContarBusquedaPorFechaAsync(DateOnly FechaInicio, DateOnly FechaFin);
        Task<int> ContarBusquedaPorUsuarioAsync(string IdUsuario);
        Task<CitaDto> CrearAsync(CitaCrearDto dto, string idUsuario);
        Task CambiarEstadoAsync(int id, string nuevoEstado);
    }
}
