using System;
using System.Collections.Generic;
using System.Text;
using Veterinaria.Domain.Entities;

namespace Veterinaria.Application.Interface.Repository
{
    public interface IMascotaRepository
    {
        Task<Mascota?> ObtenerporIdAsync(int id);
        Task<IEnumerable<Mascota>> ObtenerTodasAsync();
        Task<IEnumerable<Mascota>> BuscarMascotaAsync(string nombre);
       



        Task CrearAsync(Mascota mascota);
        Task ActualizarAsync(Mascota mascota);
        Task EliminarAsync(int id);
    }
}
