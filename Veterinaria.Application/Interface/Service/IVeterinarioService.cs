using System;
using System.Collections.Generic;
using System.Text;
using Veterinaria.Application.DTOs.Veterinario;

namespace Veterinaria.Application.Interface.Service
{
    public interface IVeterinarioService
    {
        Task<VeterinarioDto?> ObtenerporIdAsync(int id);
        Task<IEnumerable<VeterinarioDto>> ObtenerTodasAsync();
        Task<IEnumerable<VeterinarioDto>> BuscarVeterinaioAsyn(string nombre);



        Task<VeterinarioDto> CrearAsync(VeterianrioCrearDto dto, string idUsuario);
        Task<VeterinarioDto> ActualizarAsync(int id, VeterinarioActualizarDto dto);
        Task EliminarAsync(int id);
    }
}
