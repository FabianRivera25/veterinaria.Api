

using System.ComponentModel.DataAnnotations;

namespace Veterinaria.Application.DTOs.Usuario
{
    public class UsuarioRegistroDto
    {
        [Required(ErrorMessage = "El nombre del usuario es requerido.")]
        public string NombreCompleto { get; set; } = null!;

        [Required(ErrorMessage = "El email del usuario es requerido.")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "la contraseña del usuario es requerida.")]
        public string Password { get; set; } = null!;

        [Required(ErrorMessage = "El rol del usuario es requerido.")]
        public string Rol { get; set; } = null!;

        [Required(ErrorMessage = "El teléfono del usuario es requerido.")]
        public string PhoneNumber { get; set; } = null!;
    }
}
