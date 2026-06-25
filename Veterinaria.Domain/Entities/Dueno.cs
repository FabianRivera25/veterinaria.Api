
namespace Veterinaria.Domain.Entities
{
    public class Dueno
    {
        public int Id { get; set; }

        public string NombreDueno { get; set; } = null!;
        public string IdUsuario { get; set; } = null!;
        public ApplicationUser? Usuario { get; set; }

        public string Telefono { get; set; } = null!;

        public string DNI { get; set; } = null!;

        public DateOnly FechaRegistro { get; private set; } = DateOnly.FromDateTime(DateTime.Now);

     
        // Propiedad de Navegación hacia Mascotas
        public virtual ICollection<Mascota> Mascotas { get; set; } = new List<Mascota>();
        // Propiedad de navegación hacia citas
        public virtual ICollection<Cita> Citas { get; set; } = new List<Cita>();
    }
}
