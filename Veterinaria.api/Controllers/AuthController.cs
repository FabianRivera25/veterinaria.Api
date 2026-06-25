using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Veterinaria.Application.DTOs.Usuario;
using Veterinaria.Application.Interface.Service;

namespace Veterinaria.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous] //aqui decimos que todos los roles tinene acceso a estas funciones
    //[Authorize(Roles = "Cliente , Admin")] esta linea definimos los roles que tienene accesos a x funcion
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _service;
        public AuthController(IAuthService service)
        {
            _service = service;
        }


        [HttpPost("Registro")]
        public async Task<ActionResult<UsuarioDto>> Crear([FromBody] UsuarioRegistroDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var registroCreado = await _service.RegistrarUsuarioAsync(dto);
            return Ok(registroCreado);
        }

        [HttpPost("login")]
        public async Task<ActionResult<UsuarioDto>> Login([FromBody] UsuarioLoginDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var respuesta = await _service.LoginAsync(dto);
            return Ok(respuesta);
        }


        [HttpPost("refresh")]
        public async Task<ActionResult<UsuarioDto>> Refresh([FromForm] RefreshTokenDto dto)
        {
            if (dto == null || string.IsNullOrWhiteSpace(dto.Refreshtoken))
                return BadRequest("Refresh Token Requerido");

            var respuesta = await _service.RefreshTokenAsync(dto.Refreshtoken);
            return Ok(respuesta);
        }
    }
}

 
