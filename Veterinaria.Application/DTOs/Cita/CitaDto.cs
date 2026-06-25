using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Veterinaria.Application.DTOs.Cita
{
    public class CitaDto
    {
        [DefaultValue(0)]
        public int Id { get; set; }

        public string IdUsuario { get; set; } = null!;

        [DefaultValue(0)]
        public int IdDueno { get; set; }

        public string NombreDueno { get; set; } = null!;

        [DefaultValue(0)]
        public int IdVeterinario { get; set; }

        public string NombreVeterinario { get; set; } = null!;

        public string Motivo { get; set; } = null!;

        [DefaultValue("25/06/2026 12:45pm")]
        public string FechaCita { get; set; } = null!;

        public string Estado { get; set; } = null!;

        public DateOnly FechaRegistro { get; set; }

        public ICollection<DetallesCitaDto> Detalles { get; set; } = new List<DetallesCitaDto>();
    }
}
