using System;
using System.Collections.Generic;
using System.Text;
using Veterinaria.Application.DTOs.Dueno;

namespace Veterinaria.Application.Interface.Service
{
    public interface IDuenoService
    {
        Task<DuenoDto?> ObtenerporIdAsync(int id);
        Task<IEnumerable<DuenoDto>> ObtenerTodasAsync();
        Task<IEnumerable<DuenoDto>> BuscarDuenoAsyn(string nombre);



        Task<DuenoDto> CrearAsync(DuenoCrearDto dto, string idUsuario);
        Task<DuenoDto> ActualizarAsync(int id, DuenoActualizarDto dto);
        Task EliminarAsync(int id);
    }
}
