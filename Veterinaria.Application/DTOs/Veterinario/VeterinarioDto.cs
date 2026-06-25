using System;
using System.Collections.Generic;
using System.Text;
using Veterinaria.Domain.Entities;

namespace Veterinaria.Application.DTOs.Veterinario
{
    public class VeterinarioDto
    {
        public int Id { get; set; }
        //public int IdUsuario { get; set; } 
      
        public string IdUsuario { get; set; } = null!;
        public string? NombreCompleto { get; set; }

        public string NombreVeterinario { get; set; } = null!;

        public string Especialidad { get; set; } = null!;

        public string DNI { get; set; } = null!;

        public string Telefono { get; set; } = null!;
    }
}
