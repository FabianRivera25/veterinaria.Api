

namespace Veterinaria.Domain.Entities
{
    public class Veterinario
    {
        public int Id { get; set; }

        public string NombreVeterinario { get; set; } = null!;

        public string IdUsuario { get; set; } = null!;
        public ApplicationUser? Usuario { get; set; }

        public string Especialidad { get; set; } = null!;

        public string DNI { get; set; } = null!;

        public string Telefono { get; set; } = null!;

        public DateOnly FechaRegistro { get; private set; } = DateOnly.FromDateTime(DateTime.Now);

        // Propiedad de navegación
        public virtual ICollection<Cita> Citas { get; set; } = new List<Cita>();
    }
}
