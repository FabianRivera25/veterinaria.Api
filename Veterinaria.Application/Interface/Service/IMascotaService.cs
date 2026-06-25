using System;
using System.Collections.Generic;
using System.Text;
using Veterinaria.Application.DTOs.Mascota;
using Veterinaria.Domain.Entities;

namespace Veterinaria.Application.Interface.Service
{
    public interface IMascotaService
    {
        Task<MascotaDto?> ObtenerporIdAsync(int id);
        Task<IEnumerable<MascotaDto>> ObtenerTodasAsync();
        Task<IEnumerable<MascotaDto>> BuscarMascotaAsync(string nombre);




        Task <MascotaDto> CrearAsync(MascotaCrearDto dto);
        Task <MascotaDto>ActualizarAsync(int id, MascotaActualizarDto dto);
        Task EliminarAsync(int id);
    }
}
