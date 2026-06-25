using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Veterinaria.Application.DTOs.Cita
{
    public class DetallesCitaCrearDto
    {
        /// <summary>
        /// ID de la mascota que asiste a la cita.
        /// </summary>
        /// <example>0</example>
        [Required(ErrorMessage = "El ID de la mascota es requerido.")]
        [Range(1, int.MaxValue, ErrorMessage = "El ID de la mascota debe ser mayor a cero.")]
        [DefaultValue(0)]
        public int MacotaId { get; set; } = 0;
    }
}
