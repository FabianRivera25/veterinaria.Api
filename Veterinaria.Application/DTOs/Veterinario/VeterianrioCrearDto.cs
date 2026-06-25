using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;

namespace Veterinaria.Application.DTOs.Veterinario
{
    public class VeterianrioCrearDto
    {

        //[JsonIgnore]
        //public string? IdUsuario { get; set; } = null!;


        [Required(ErrorMessage = "El nombre del Veterinario es requerido.")]
        [MaxLength(40, ErrorMessage = "El nombre del Veterinario no puede exceder los 40 caracteres.")]
        public string NombreVeterinario { get; set; } = null!;

        [Required(ErrorMessage = "La especilidad del Veterinario es requerido.")]
        [MaxLength(30, ErrorMessage = "La especialidad  no puede exceder los 30 caracteres.")]
        public string Especialidad { get; set; } = null!;

        [Required(ErrorMessage = "EL DNI del Veterinario es requerido.")]
        [MaxLength(25, ErrorMessage = "La DNI  no puede exceder los 25 caracteres.")]
        public string DNI { get; set; } = null!;


        [Required(ErrorMessage = "El telefono del Veterinario es requerido.")]
        [MaxLength(20, ErrorMessage = "El telefono  del veterinario no puede exceder los 20 caracteres.")]
        public string Telefono { get; set; } = null!;

       


    }
}
