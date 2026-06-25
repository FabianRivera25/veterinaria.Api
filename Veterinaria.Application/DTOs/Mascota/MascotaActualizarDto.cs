using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Veterinaria.Application.DTOs.Mascota
{
    public class MascotaActualizarDto
    {

        [Required(ErrorMessage = "El nombre de la mascota es requerido.")]
        [MaxLength(25, ErrorMessage = "El nombre de la mascota no puede exceder los 25 caracteres.")]
        public string NombreMascota { get; set; } = null!;

        [Required(ErrorMessage = "El nombre de la especie es requerido.")]
        [MaxLength(20, ErrorMessage = "La especie no puede exceder los 20 caracteres.")]
        public string Especie { get; set; } = null!;

        [Required(ErrorMessage = "El nombre de la raza de la mascota es requerido.")]
        [MaxLength(20, ErrorMessage = "El nombre de la raza  no puede exceder los 20 caracteres.")]
        public string Raza { get; set; } = null!;


        [Required(ErrorMessage = "La edad de la mascota es requerido. ejemplo = 2 meses.")]
        [MaxLength(20, ErrorMessage = "La edada de la mascota  no puede exceder los 20 caracteres.")]
        public string Edad { get; set; } = null!;



        [Required(ErrorMessage = "Es necesario especificar el sexo de la mascota.")]
        public string Sexo { get; set; } = null!;

        [Required(ErrorMessage = "Se requiere el id del dueño de la mascota")]
        public int IdDueno { get; set; }
    }
}
