using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Veterinaria.Application.DTOs.Mascota;
using Veterinaria.Application.Interface.Repository;
using Veterinaria.Application.Interface.Service;
using Veterinaria.Domain.Entities;

namespace Veterinaria.Application.Service
{
    public class MascotaService : IMascotaService
    {
        private readonly IMascotaRepository _repository;
        private readonly IMapper _mapper;

        public MascotaService(IMascotaRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<MascotaDto> ActualizarAsync(int id, MascotaActualizarDto dto)
        {
            var registro = await _repository.ObtenerporIdAsync(id);
            if (registro == null)
                throw new KeyNotFoundException("El registro no existe o fue eliminado.");

            var nuevoNombre = dto.NombreMascota.Trim();
            var nuevaEspecie = dto.Especie.Trim();
            var nuevaRaza = dto.Raza.Trim();
            var nuevaEdad = dto.Edad.Trim();
            var nuevoSexo = dto.Sexo.Trim();



            //validar duplicados solo si el nombre cambio
            //if (string.Equals(registro.Titulo.Trim(), nuevoNombre, StringComparison.OrdinalIgnoreCase))
            //{
            //var siExiste = await _repository.ExisteNombreAsync(nuevoNombre);
            //if (siExiste)
            //throw new InvalidOperationException($"Ya existe un registro con el nombre: '{dto.Titulo}'");
            //}

            _mapper.Map(dto, registro);
            await _repository.ActualizarAsync(registro);

            return _mapper.Map<MascotaDto>(registro); ;
        }

        public async Task<IEnumerable<MascotaDto>> BuscarMascotaAsync(string nombre)
        {
            var registros = await _repository.BuscarMascotaAsync(nombre);
            return _mapper.Map<IEnumerable<MascotaDto>>(registros);  
        }

        public async Task<MascotaDto> CrearAsync(MascotaCrearDto dto)
        {
            //var siExiste = await _repository.ExisteNombreAsync(dto.Titulo);
            //if (siExiste)
            //throw new InvalidOperationException($"Ya existe un registro con el nombre: '{dto.Titulo}'");

            var registro = _mapper.Map<Mascota>(dto);
            await _repository.CrearAsync(registro);

            return _mapper.Map<MascotaDto>(registro);
        }

        public async Task EliminarAsync(int id)
        {
            var registro = await _repository.ObtenerporIdAsync(id);
            if (registro == null)
                throw new KeyNotFoundException("El registro no existe o fue eliminado.");


            await _repository.EliminarAsync(id);
        }

        public async Task<MascotaDto?> ObtenerporIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID debe ser un número entero mayor a cero.");

            var registro = await _repository.ObtenerporIdAsync(id);
            if (registro == null)
                throw new KeyNotFoundException("El registro no existe o fue eliminado.");


            return _mapper.Map<MascotaDto>(registro);
        }

        public async Task<IEnumerable<MascotaDto>> ObtenerTodasAsync()
        {
            var registros = await _repository.ObtenerTodasAsync();
            return _mapper.Map<IEnumerable<MascotaDto>>(registros);
        }
    }
}
