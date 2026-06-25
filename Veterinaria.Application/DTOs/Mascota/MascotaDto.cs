using System;
using System.Collections.Generic;
using System.Text;

namespace Veterinaria.Application.DTOs.Mascota
{
    public class MascotaDto
    {
        public int Id { get; set; }

        public int IdDueno { get; set; }
        public string? NombreDueno { get; set; }

        public string NombreMascota { get; set; } = null!;

        public string Especie { get; set; } = null!;

        public string? Raza { get; set; } = null!;

        // Edad como string para permitir "2 meses", "Aprox 1 año", etc.
        public string Edad { get; set; } = null!;

        public string Sexo { get; set; } = null!;

    }
}
