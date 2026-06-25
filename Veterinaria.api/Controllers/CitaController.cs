using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Veterinaria.Application.DTOs.Cita;
using Veterinaria.Application.Interface.Service;

namespace Veterinaria.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitaController : ControllerBase
    {
        private readonly ICitaService _citaService;

        public CitaController(ICitaService citaService)
        {
            _citaService = citaService;
        }

        [HttpGet("ObtenerTodas")]
        [Authorize(Roles = "Admin, Cliente, Veterinario")]
        public async Task<ActionResult<IEnumerable<CitaDto>>> ObtenerTodas(
            [FromQuery] string? filtro = null, 
            [FromQuery] int pagina = 1, 
            [FromQuery] int tamano = 10)
        {
            var citas = await _citaService.ObtenerCitasAsync(filtro, pagina, tamano);
            var total = await _citaService.ContarAsync(filtro);

            Response.Headers.Append("X-Total-Count", total.ToString());
            return Ok(citas);
        }

        [HttpGet("{id:int}")]
        [Authorize(Roles = "Admin, Cliente, Veterinario")]
        public async Task<ActionResult<CitaDto>> ObtenerPorId(int id)
        {
            var cita = await _citaService.ObtenerporIdAsync(id);
            return Ok(cita);
        }

        [HttpGet("buscar-por-fecha")]
        [Authorize(Roles = "Admin, Cliente, Veterinario")]
        public async Task<ActionResult<IEnumerable<CitaDto>>> BuscarPorFecha(
            [FromQuery] DateOnly fechaInicio, 
            [FromQuery] DateOnly fechaFin, 
            [FromQuery] int pagina = 1, 
            [FromQuery] int tamano = 10)
        {
            var citas = await _citaService.BuscarPorFechaAsync(fechaInicio, fechaFin, pagina, tamano);
            var total = await _citaService.ContarBusquedaPorFechaAsync(fechaInicio, fechaFin);

            Response.Headers.Append("X-Total-Count", total.ToString());
            return Ok(citas);
        }

        [HttpGet("mis-citas")]
        [Authorize(Roles = "Admin, Cliente, Veterinario")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<CitaDto>>> MisCitas(
            [FromQuery] int pagina = 1, 
            [FromQuery] int tamano = 10)
        {
            var idUsuario = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrWhiteSpace(idUsuario))
                return Unauthorized("Usuario no autenticado.");

            var citas = await _citaService.BuscarPorUsuarioAsync(idUsuario, pagina, tamano);
            var total = await _citaService.ContarBusquedaPorUsuarioAsync(idUsuario);

            Response.Headers.Append("X-Total-Count", total.ToString());
            return Ok(citas);
        }

        [HttpPost("CrearCita")]
        [Authorize(Roles = "Admin, Cliente, Veterinario")]
        [Authorize]
        public async Task<ActionResult<CitaDto>> CrearCita([FromBody] CitaCrearDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var idUsuario = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrWhiteSpace(idUsuario))
                return Unauthorized("Usuario no autenticado.");

            var nuevaCita = await _citaService.CrearAsync(dto, idUsuario);
            return CreatedAtAction(nameof(ObtenerPorId), new { id = nuevaCita.Id }, nuevaCita);
        }

        [HttpPut("cambiar-estado/{id:int}")]
        [Authorize(Roles = "Admin, Cliente, Veterinario")]
        public async Task<ActionResult> CambiarEstado(int id, [FromQuery] string nuevoEstado)
        {
            await _citaService.CambiarEstadoAsync(id, nuevoEstado);
            return NoContent();
        }
    }
}
