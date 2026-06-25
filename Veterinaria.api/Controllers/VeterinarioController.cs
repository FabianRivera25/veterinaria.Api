using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NpgsqlTypes;
using Veterinaria.Application.DTOs.Veterinario;
using Veterinaria.Application.Interface.Service;

namespace Veterinaria.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VeterinarioController : ControllerBase
    {
        private readonly IVeterinarioService _VeterinarioService;

        public VeterinarioController(IVeterinarioService veterinarioService)
        {
            _VeterinarioService = veterinarioService;
        }

        [HttpGet ("ObtenerTodos")]
        [Authorize(Roles = "Admin, Veterinario, Cliente")]
        public async Task<ActionResult<ICollection<VeterinarioDto>>> ObtenerTodas()
        {
            var idUsuario = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrWhiteSpace(idUsuario))
                return Unauthorized("Usuario no autenticado");

            var veterinarios = await _VeterinarioService.ObtenerTodasAsync();
            if (veterinarios == null || !veterinarios.Any())
                return NotFound("No hay categorias registradas.");

            return Ok(veterinarios);
        }

        [HttpGet("{id:int}", Name = "obterner por id")]
        [Authorize(Roles = "Admin, Veterinario, Cliente")]
        public async Task<ActionResult<VeterinarioDto>> ObtenerPorId(int id)
        {
            var idUsuario = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrWhiteSpace(idUsuario))
                return Unauthorized("Usuario no autenticado");

            var categoria = await _VeterinarioService.ObtenerporIdAsync(id);
            return Ok(categoria);
        }

        [HttpPost("Crear Veterinario")]
        [Authorize(Roles = "Admin, Veterinario")]
        public async Task<ActionResult<VeterinarioDto>> crear([FromBody] VeterianrioCrearDto dto)
        {
            

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var idUsuario = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrWhiteSpace(idUsuario))
                return Unauthorized("Usuario no autenticado");

            

            var nuevoVeterinario = await _VeterinarioService.CrearAsync(dto, idUsuario);
            return Ok(nuevoVeterinario);
        }

        [HttpPut("{id:int}", Name = "Actualizar Veterinario")]
        [Authorize(Roles = "Admin, Veterinario")]
        public async Task<ActionResult> Actualizar(int id, [FromBody] VeterinarioActualizarDto dto)
        {
            var usuarioId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrWhiteSpace(usuarioId))
                return Unauthorized("Usuario no autenticado");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var categoriaActualizar = await _VeterinarioService.ActualizarAsync(id, dto);
            return Ok(categoriaActualizar);
        }

        [HttpDelete("{id:int}", Name = "Borrar Veterinario")]
        [Authorize(Roles = "Admin, Veterinario")]
        public async Task<ActionResult> Eliminar(int id)
        {
            var usuarioId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrWhiteSpace(usuarioId))
                return Unauthorized("Usuario no autenticado");

            await _VeterinarioService.EliminarAsync(id);
            return NoContent();
        }

    }
}
