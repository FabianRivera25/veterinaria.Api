using System;
using System.Collections.Generic;
using System.Text;
using Veterinaria.Application.DTOs.Usuario;
using Veterinaria.Application.Response;

namespace Veterinaria.Application.Interface.Service
{
    public interface IAuthService
    {
        Task<RespuestaLoginDto> LoginAsync(UsuarioLoginDto dto);
        Task<UsuarioDto> RegistrarUsuarioAsync(UsuarioRegistroDto dto);
        Task<RespuestaLoginDto> RefreshTokenAsync(string refreshToken);
    }
}
