

namespace Veterinaria.Domain.Entities
{
    public class Cita
    {
        public int Id { get; set; }

        public string IdUsuario { get; set; } = null!;
        public  ApplicationUser? Usuario { get; set; }

        public int IdDueno { get; set; } 
        public Dueno  Dueno { get; set; } = null !;

        //public int IdMascota { get; set; }
        //public Mascota Mascota { get; set; } = null!;

        public int IdVeterinario { get; set; }
        public Veterinario Veterinario { get; set; } = null!;

        public string Motivo { get; set; } = null!;

        // Usamos DateTime para guardar tanto la fecha como la hora
        public DateTime FechaCita { get; set; }

        //public bool Estado { get; set; } = true;
        public string Estado { get; set; } = "Activo";

        public DateOnly FechaRegistro { get; private set; } = DateOnly.FromDateTime(DateTime.Now);

        // Propiedad de navegación a detalles de cita
        public virtual ICollection<DetallesCita> Detalles { get; set; } = new List<DetallesCita>();
    }
}
