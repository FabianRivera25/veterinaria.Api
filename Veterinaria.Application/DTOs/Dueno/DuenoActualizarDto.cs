using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Veterinaria.Application.DTOs.Dueno
{
    public class DuenoActualizarDto
    {
        [Required(ErrorMessage = "El nombre del Dueño es requerido.")]
        [MaxLength(40, ErrorMessage = "El nombre del Dueño no puede exceder los 40 caracteres.")]
        public string NombreDueno { get; set; } = null!;

        [Required(ErrorMessage = "El Telefono del Dueño es requerido.")]
        [MaxLength(20, ErrorMessage = "La nacionalidad  no puede exceder los 20 caracteres.")]
        public string Telefono { get; set; } = null!;

        [Required(ErrorMessage = "El DNI  es requerido.")]
        [MaxLength(25, ErrorMessage = "El DNI no puede exceder los 25 caracteres")]
        public string DNI { get; set; } = null!;

        //public string IdUsuario { get; set; } = null!;
    }
}
