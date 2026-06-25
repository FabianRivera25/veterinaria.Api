using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Veterinaria.Application.DTOs.Cita
{
    public class CitaCrearDto
    {
        /// <summary>
        /// ID del dueño de la mascota.
        /// </summary>
        /// <example>0</example>
        [Required(ErrorMessage = "El ID del dueño es requerido.")]
        [Range(1, int.MaxValue, ErrorMessage = "El ID del dueño debe ser mayor a cero.")]
        [DefaultValue(0)]
        public int IdDueno { get; set; } = 0;

        /// <summary>
        /// ID del veterinario asignado.
        /// </summary>
        /// <example>0</example>
        [Required(ErrorMessage = "El ID del veterinario es requerido.")]
        [Range(1, int.MaxValue, ErrorMessage = "El ID del veterinario debe ser mayor a cero.")]
        [DefaultValue(0)]
        public int IdVeterinario { get; set; } = 0;

        /// <summary>
        /// Motivo de la cita.
        /// </summary>
        /// <example>Consulta de control y vacunas</example>
        [Required(AllowEmptyStrings = false, ErrorMessage = "El motivo de la cita es requerido.")]
        [StringLength(250, ErrorMessage = "El motivo no puede exceder los 250 caracteres.")]
        public string Motivo { get; set; } = null!;

        /// <summary>
        /// Fecha y hora de la cita. Formato esperado: dd/MM/yyyy hh:mmtt
        /// </summary>
        /// <example>25/06/2026 12:45pm</example>
        [Required(ErrorMessage = "La fecha y hora de la cita son requeridas.")]
        [DefaultValue("25/06/2026 12:45pm")]
        public string FechaCita { get; set; } = "25/06/2026 12:45pm";

        /// <summary>
        /// Listado de mascotas asociadas a la cita.
        /// </summary>
        [Required(ErrorMessage = "Debe especificar al menos un detalle para la cita.")]
        public ICollection<DetallesCitaCrearDto> Detalles { get; set; } = new List<DetallesCitaCrearDto>();
    }
}
