using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Veterinaria.Application.DTOs.Dueno;
using Veterinaria.Application.DTOs.Usuario;
using Veterinaria.Application.DTOs.Veterinario;
using Veterinaria.Application.Interface.Repository;
using Veterinaria.Application.Interface.Service;
using Veterinaria.Application.Service;
using Veterinaria.Domain.Entities;

namespace Veterinaria.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DuenoController : ControllerBase
    {
        private readonly IDuenoService _duenoService;

        public DuenoController(IDuenoService duenoService)
        {
            _duenoService = duenoService;
        }


        [HttpGet("ObtenerTodos")]
        [Authorize(Roles = "Admin, Cliente, Veterinario")]
        public async Task<ActionResult<ICollection<DuenoDto>>> ObtenerTodas()
        {
            var idUsuario = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrWhiteSpace(idUsuario))
                return Unauthorized("Usuario no autenticado");

            var duenos = await _duenoService.ObtenerTodasAsync();
            if (duenos == null || !duenos.Any())
                return NotFound("No hay Dueños registradas.");

            return Ok(duenos);
        }

        [HttpGet("{id:int}", Name = "ObtenerPorId")]
        [Authorize(Roles = "Admin, Cliente, Veterinario")]
        public async Task<ActionResult<DuenoDto>> ObtenerPorId(int id)
        {
            var idUsuario = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrWhiteSpace(idUsuario))
                return Unauthorized("Usuario no autenticado");

            var dueno = await _duenoService.ObtenerporIdAsync(id);
            return Ok(dueno);
        }

        [HttpPost("Crear Dueno")]
        [Authorize(Roles = "Admin, Cliente")]
        [Authorize]
        public async Task<ActionResult<DuenoDto>> crear([FromBody] DuenoCrearDto dto)
        {


            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var idUsuario = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrWhiteSpace(idUsuario))
                return Unauthorized("Usuario no autenticado");

           

            var nuevoDueno = await _duenoService.CrearAsync(dto, idUsuario);
            return Ok(nuevoDueno);
        }

        [HttpPut("{id:int}", Name = "ActualizarDueno")]
        [Authorize(Roles = "Admin, Cliente")]
        public async Task<ActionResult> Actualizar(int id, [FromBody] DuenoActualizarDto dto)
        {
            var idUsuario = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrWhiteSpace(idUsuario))
                return Unauthorized("Usuario no autenticado");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var duenoActualizar = await _duenoService.ActualizarAsync(id, dto);
            return Ok(duenoActualizar);
        }

        [HttpDelete("{id:int}", Name = "Borrar dueno")]
        [Authorize(Roles = "Admin, Cliente")]
        public async Task<ActionResult> Eliminar(int id)
        {
            var idUsuario = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrWhiteSpace(idUsuario))
                return Unauthorized("Usuario no autenticado");

            await _duenoService.EliminarAsync(id);
            return NoContent();
        }

    }
}
