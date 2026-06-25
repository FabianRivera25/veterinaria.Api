using System;
using System.Collections.Generic;
using System.Text;
using Veterinaria.Domain.Entities;

namespace Veterinaria.Application.Interface.Repository
{
    public interface IVeterinarioRepository
    {
        Task<Veterinario?> ObtenerporIdAsync(int id);
        Task<IEnumerable<Veterinario>> ObtenerTodasAsync();
        Task<IEnumerable<Veterinario>> BuscarVeterinarioAsync(string nombre);
        Task<bool> ExisteDniAsync(string DNI);



        Task CrearAsync(Veterinario veterinario);
        Task ActualizarAsync(Veterinario veterinario);
        Task EliminarAsync(int id);
    }
}
