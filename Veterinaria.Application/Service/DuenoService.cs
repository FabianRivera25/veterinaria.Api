using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Veterinaria.Application.DTOs.Dueno;
using Veterinaria.Application.DTOs.Veterinario;
using Veterinaria.Application.Interface.Repository;
using Veterinaria.Application.Interface.Service;
using Veterinaria.Domain.Entities;

namespace Veterinaria.Application.Service
{
    public class DuenoService : IDuenoService
    {
        private readonly IDuenoRepository _repository;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public DuenoService(IDuenoRepository repository, UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<DuenoDto> ActualizarAsync(int id, DuenoActualizarDto dto)
        {
            var registro = await _repository.ObtenerporIdAsync(id);
            if (registro == null)
                throw new KeyNotFoundException("El registro no existe o fue eliminado.");

            var nuevoNombre = dto.NombreDueno.Trim();
            var nuevoTelefono = dto.Telefono.Trim();
            var nuevoDni = dto.DNI.Trim();


            //validar duplicados solo si el nombre cambio
            if (string.Equals(registro.DNI.Trim(), nuevoDni, StringComparison.OrdinalIgnoreCase))
            {
                var siExiste = await _repository.ExisteDniAsync(nuevoDni);
                if (siExiste)
                    throw new InvalidOperationException($"Ya existe un registro con el Dni: '{dto.DNI}'");
            }

            _mapper.Map(dto, registro);
            await _repository.ActualizarAsync(registro);

            return _mapper.Map<DuenoDto>(registro);
        }

     

        public async Task<IEnumerable<DuenoDto>> BuscarDuenoAsyn(string nombre)
        {
            var registros = await _repository.BuscarDuenoAsync(nombre);
            return _mapper.Map<IEnumerable<DuenoDto>>(registros);
        }

        public async Task<DuenoDto> CrearAsync(DuenoCrearDto dto, string idUsuario)
        {
            var siExiste = await _repository.ExisteDniAsync(dto.DNI);
            if (siExiste)
                throw new InvalidOperationException($"Ya existe un registro con el DNI: '{dto.DNI}'");

            var usuario = await _userManager.FindByIdAsync(idUsuario);
            if (usuario == null)
                throw new KeyNotFoundException("Usuario no encontrado");



            var registro = _mapper.Map<Dueno>(dto);
            registro.IdUsuario = idUsuario;
            await _repository.CrearAsync(registro);

            return _mapper.Map<DuenoDto>(registro);
        }

        public async Task EliminarAsync(int id)
        {
            var registro = await _repository.ObtenerporIdAsync(id);
            if (registro == null)
                throw new KeyNotFoundException("El registro no existe o fue eliminado.");

            if (registro.Citas.Any())
                throw new InvalidOperationException($"no se puede eliminar  al veterinario: '{registro.NombreDueno}' por que tiene citas asociadas.");

            await _repository.EliminarAsync(id);
        }

        public async Task<DuenoDto?> ObtenerporIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID debe ser un número entero mayor a cero.");

            var registro = await _repository.ObtenerporIdAsync(id);
            if (registro == null)
                throw new KeyNotFoundException("El registro no existe o fue eliminado.");

            return _mapper.Map<DuenoDto>(registro);
        }

        public async Task<IEnumerable<DuenoDto>> ObtenerTodasAsync()
        {
            var registros = await _repository.ObtenerTodasAsync();
            return _mapper.Map<IEnumerable<DuenoDto>>(registros);
        }
    }
}
