using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Veterinaria.Application.DTOs.Usuario
{
    public class UsuarioLoginDto
    {
        [Required(ErrorMessage = "El email del usuario es requerido.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "la contraseña del usuario es requerida.")]
        public string Password { get; set; } = string.Empty;
    }
}
