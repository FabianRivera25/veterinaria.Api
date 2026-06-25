
namespace Veterinaria.Domain.Entities
{
    public class Mascota
    {
        public int Id { get; set; }

        public int IdDueno { get; set; }
        public Dueno Dueno { get; set; } = null!;

        public string NombreMascota { get; set; } = null!;

        public string Especie { get; set; } = null!;

        public string? Raza { get; set; } = null!;

        // Edad como string para permitir "2 meses", "Aprox 1 año", etc.
        public string Edad { get; set; } = null!;

        public string Sexo { get; set; } = null!;

        public DateOnly FechaRegistro { get; private set; } = DateOnly.FromDateTime(DateTime.Now);

        // navegacion a detalles de cita
        public virtual ICollection<DetallesCita> Detalles { get; set; } = new List<DetallesCita>();

    }
}
