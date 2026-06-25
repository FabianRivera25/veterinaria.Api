using System;
using System.Collections.Generic;
using System.Text;
using Veterinaria.Domain.Entities;

namespace Veterinaria.Application.DTOs.Dueno
{
    public class DuenoDto
    {
        public int Id { get; set; }
        public string IdUsuario { get; set; } = null!;
        public string? NombreCompleto { get; set; } = null!;
        public string NombreDueno { get; set; } = null!;
        public string Telefono { get; set; } = null!;
        public string DNI { get; set; } = null!;

    }
}
