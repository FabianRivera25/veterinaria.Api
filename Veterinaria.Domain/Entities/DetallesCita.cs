using System;
using System.Collections.Generic;
using System.Text;

namespace Veterinaria.Domain.Entities
{
    public class DetallesCita
    {
        public int Id { get; set; }

        public int CitaID { get; set; }
        public Cita? Cita { get; set; }

        public int MacotaId { get; set; }
        public Mascota? Mascota { get; set; }
        public string NombreMascota { get; set; } = null!;


    }
}
