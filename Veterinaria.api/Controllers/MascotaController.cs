using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Veterinaria.Application.DTOs.Mascota;
using Veterinaria.Application.Interface.Service;

namespace Veterinaria.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MascotaController : ControllerBase
    {
        private readonly IMascotaService _macotaService;

        public MascotaController(IMascotaService mascotaService)
        {
            _macotaService = mascotaService;
        }

        [HttpGet("Obtener Todos")]
        [Authorize(Roles = "Admin, Cliente, Veterinario")]
        public async Task<ActionResult<ICollection<MascotaDto>>> ObtenerTodas()
        {
            var mascotas = await _macotaService.ObtenerTodasAsync();
            if (mascotas == null || !mascotas.Any())
                return NotFound("No hay mascotas registradas.");

            return Ok(mascotas);
        }

        [HttpGet("{id:int}", Name = "Obtener por Id")]
        [Authorize(Roles = "Admin, Cliente, Veterinario")]
        public async Task<ActionResult<MascotaDto>> ObtenerPorId(int id)
        {
            var categoria = await _macotaService.ObtenerporIdAsync(id);
            return Ok(categoria);
        }

        [HttpPost("Crear Mascota")]
        [Authorize(Roles = "Admin, Cliente")]
        public async Task<ActionResult<MascotaDto>> crear([FromBody] MascotaCrearDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var nuevaMascota = await _macotaService.CrearAsync(dto);
            return Ok(nuevaMascota);
        }

        [HttpPut("{id:int}", Name = "Actualizar mascota")]
        [Authorize(Roles = "Admin, Cliente")]
        public async Task<ActionResult> Actualizar(int id, [FromBody] MascotaActualizarDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var mascotaActualizar = await _macotaService.ActualizarAsync(id, dto);
            return Ok(mascotaActualizar);
        }

        [HttpDelete("{id:int}", Name = "Eliminar Mascota")]
        [Authorize(Roles = "Admin, Cliente")]
        public async Task<ActionResult> Eliminar(int id)
        {
            await _macotaService.EliminarAsync(id);
            return NoContent();
        }
    }
}

