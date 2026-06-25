using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Veterinaria.Application.DTOs.Cita;
using Veterinaria.Application.Interface.Repository;
using Veterinaria.Application.Interface.Service;
using Veterinaria.Domain.Entities;

namespace Veterinaria.Application.Service
{
    public class CitaService : ICitaService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public CitaService(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<CitaDto> ObtenerporIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID debe ser un número entero mayor a cero.");

            var cita = await _unitOfWork.Citas.ObtenerporIdAsync(id);
            if (cita == null)
                throw new KeyNotFoundException("La cita no existe o fue eliminada.");

            return _mapper.Map<CitaDto>(cita);
        }

        public async Task<IEnumerable<CitaDto>> ObtenerCitasAsync(string? filtro, int pagina, int tamano)
        {
            var citas = await _unitOfWork.Citas.ObtenerCitasAsync(filtro, pagina, tamano);
            return _mapper.Map<IEnumerable<CitaDto>>(citas);
        }

        public async Task<IEnumerable<CitaDto>> BuscarPorFechaAsync(DateOnly FechaInicio, DateOnly FechaFin, int pagina, int tamano)
        {
            if (FechaInicio > FechaFin)
                throw new ArgumentException("La fecha de inicio no puede ser mayor que la fecha final.");

            var citas = await _unitOfWork.Citas.BuscarPorFechaAsync(FechaInicio, FechaFin, pagina, tamano);
            return _mapper.Map<IEnumerable<CitaDto>>(citas);
        }

        public async Task<IEnumerable<CitaDto>> BuscarPorUsuarioAsync(string IdUsuario, int pagina, int tamano)
        {
            if (string.IsNullOrWhiteSpace(IdUsuario))
                throw new ArgumentException("El ID del usuario no puede ser nulo o vacío.");

            var citas = await _unitOfWork.Citas.BuscarPorUsuariosync(IdUsuario, pagina, tamano);
            return _mapper.Map<IEnumerable<CitaDto>>(citas);
        }

        public async Task<int> ContarAsync(string? filtro)
        {
            return await _unitOfWork.Citas.ContarAsync(filtro);
        }

        public async Task<int> ContarBusquedaPorFechaAsync(DateOnly FechaInicio, DateOnly FechaFin)
        {
            if (FechaInicio > FechaFin)
                throw new ArgumentException("La fecha de inicio no puede ser mayor que la fecha final.");

            return await _unitOfWork.Citas.ContarBusquedaPorFechaAsync(FechaInicio, FechaFin);
        }

        public async Task<int> ContarBusquedaPorUsuarioAsync(string IdUsuario)
        {
            if (string.IsNullOrWhiteSpace(IdUsuario))
                throw new ArgumentException("El ID del usuario no puede ser nulo o vacío.");

            return await _unitOfWork.Citas.ContarBusquedaPorUsuarioAsync(IdUsuario);
        }

        public async Task<CitaDto> CrearAsync(CitaCrearDto dto, string idUsuario)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto), "Los datos de la cita no pueden ser nulos.");

            if (string.IsNullOrWhiteSpace(idUsuario))
                throw new ArgumentException("El ID del usuario no puede ser nulo o vacío.", nameof(idUsuario));

            if (string.IsNullOrWhiteSpace(dto.Motivo))
                throw new InvalidOperationException("El motivo de la cita es requerido.");

            // Parsear la fecha y hora con el formato am/pm
            DateTime fechaCitaParsed;
            try
            {
                fechaCitaParsed = ParseFechaCita(dto.FechaCita);
            }
            catch (ArgumentException ex)
            {
                throw new InvalidOperationException(ex.Message);
            }

            // 1. Validar usuario
            var usuario = await _userManager.FindByIdAsync(idUsuario);
            if (usuario == null)
                throw new KeyNotFoundException("Usuario no encontrado.");

            // 2. Validar dueño
            var dueno = await _unitOfWork.Duenos.ObtenerporIdAsync(dto.IdDueno);
            if (dueno == null)
                throw new KeyNotFoundException("Dueño no encontrado.");

            // 3. Validar veterinario
            var veterinario = await _unitOfWork.Veterinarios.ObtenerporIdAsync(dto.IdVeterinario);
            if (veterinario == null)
                throw new KeyNotFoundException("Veterinario no encontrado.");

            // 4. Validar detalles de mascotas
            if (dto.Detalles == null || !dto.Detalles.Any())
                throw new InvalidOperationException("Debe especificar al menos un detalle para la cita.");

            var petIds = dto.Detalles.Select(d => d.MacotaId).ToList();
            if (petIds.Count != petIds.Distinct().Count())
                throw new InvalidOperationException("No se permiten mascotas repetidas en la cita.");

            // 5. Validar cada mascota y que pertenezca al dueño
            foreach (var detailDto in dto.Detalles)
            {
                var mascota = await _unitOfWork.Mascotas.ObtenerporIdAsync(detailDto.MacotaId);
                if (mascota == null)
                    throw new KeyNotFoundException($"La mascota con ID {detailDto.MacotaId} no existe.");

                if (mascota.IdDueno != dto.IdDueno)
                    throw new InvalidOperationException($"La mascota '{mascota.NombreMascota}' no pertenece al dueño especificado.");
            }

            // 6. Validar restricción de horario (clash check)
            bool existeClash = await _unitOfWork.Citas.ExisteCitaEnFechaAsync(fechaCitaParsed);
            if (existeClash)
                throw new InvalidOperationException("ya existe una cita agendada a esta hora");

            // 7. Mapear y guardar cita
            var cita = _mapper.Map<Cita>(dto);
            cita.IdUsuario = idUsuario;
            cita.FechaCita = fechaCitaParsed;
            cita.Estado = "Activo";

            // Rellenar NombreMascota a partir de la base de datos
            cita.Detalles.Clear();
            foreach (var detailDto in dto.Detalles)
            {
                var mascota = await _unitOfWork.Mascotas.ObtenerporIdAsync(detailDto.MacotaId);
                cita.Detalles.Add(new DetallesCita
                {
                    MacotaId = detailDto.MacotaId,
                    NombreMascota = mascota!.NombreMascota
                });
            }

            await _unitOfWork.BeginTransactionAsync();
            try
            {
                await _unitOfWork.Citas.CrearAsync(cita);
                await _unitOfWork.CommitAsync();
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }

            // Asignar navegación para que AutoMapper los mapee correctamente en la respuesta
            cita.Dueno = dueno;
            cita.Veterinario = veterinario;

            return _mapper.Map<CitaDto>(cita);
        }

        public async Task CambiarEstadoAsync(int id, string nuevoEstado)
        {
            if (id <= 0)
                throw new ArgumentException("El ID debe ser un número entero mayor a cero.");

            if (nuevoEstado != "Activo" && nuevoEstado != "Inactivo")
                throw new ArgumentException("El estado debe ser 'Activo' o 'Inactivo'.");

            var cita = await _unitOfWork.Citas.ObtenerporIdAsync(id);
            if (cita == null)
                throw new KeyNotFoundException("La cita no existe o fue eliminada.");

            // Si se reactiva la cita, se debe validar que no haya choque de horas
            if (nuevoEstado == "Activo" && cita.Estado != "Activo")
            {
                bool existeClash = await _unitOfWork.Citas.ExisteCitaEnFechaAsync(cita.FechaCita, cita.Id);
                if (existeClash)
                    throw new InvalidOperationException("ya existe una cita agendada a esta hora");
            }

            cita.Estado = nuevoEstado;

            await _unitOfWork.BeginTransactionAsync();
            try
            {
                await _unitOfWork.Citas.CambiarEstado(cita);
                await _unitOfWork.CommitAsync();
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }
        }

        private DateTime ParseFechaCita(string fechaStr)
        {
            if (string.IsNullOrWhiteSpace(fechaStr))
                throw new ArgumentException("La fecha y hora de la cita no pueden estar vacías.");

            // Normalizar el formato quitando espacios adicionales y estandarizando am/pm
            string normalized = fechaStr.Trim()
                .Replace("p.m.", "pm")
                .Replace("a.m.", "am")
                .Replace("P.M.", "pm")
                .Replace("A.M.", "am")
                .Replace(" ", ""); // remover espacios temporales para verificar formatos sin espacios

            string[] formats = new[] {
                "dd/MM/yyyyhh:mmtt",
                "dd/MM/yyyyHH:mm",
                "dd/MM/yyhh:mmtt",
                "dd/MM/yyHH:mm",
                "yyyy-MM-ddHH:mm",
                "yyyy-MM-ddhh:mmtt"
            };

            if (DateTime.TryParseExact(normalized, formats, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out DateTime parsedDate))
            {
                return parsedDate;
            }

            // Normalizar con un espacio para verificar formatos que sí tienen espacio antes de am/pm
            string normalizedWithSpace = fechaStr.Trim()
                .Replace("p.m.", "pm")
                .Replace("a.m.", "am")
                .Replace("P.M.", "pm")
                .Replace("A.M.", "am");

            string[] formatsWithSpace = new[] {
                "dd/MM/yyyy hh:mm tt",
                "dd/MM/yyyy HH:mm",
                "dd/MM/yy hh:mm tt",
                "dd/MM/yy HH:mm",
                "yyyy-MM-dd HH:mm",
                "yyyy-MM-dd hh:mm tt"
            };

            if (DateTime.TryParseExact(normalizedWithSpace, formatsWithSpace, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out parsedDate))
            {
                return parsedDate;
            }

            // Fallback al TryParse general de C#
            if (DateTime.TryParse(fechaStr, out DateTime fallbackDate))
            {
                return fallbackDate;
            }

            throw new ArgumentException("El formato de fecha y hora no es válido. Ejemplo esperado: '26/05/2026 12:45pm'.");
        }
    }
}
