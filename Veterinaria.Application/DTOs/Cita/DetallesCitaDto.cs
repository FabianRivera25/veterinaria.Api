using System.ComponentModel;

namespace Veterinaria.Application.DTOs.Cita
{
    public class DetallesCitaDto
    {
        [DefaultValue(0)]
        public int Id { get; set; }

        [DefaultValue(0)]
        public int CitaID { get; set; }

        [DefaultValue(0)]
        public int MacotaId { get; set; }

        public string NombreMascota { get; set; } = null!;
    }
}
