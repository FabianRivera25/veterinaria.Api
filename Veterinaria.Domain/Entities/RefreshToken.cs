
namespace Veterinaria.Domain.Entities
{
    public class RefreshToken
    { 
        public int id { get; set; }
        public string Token { get; set; } = null!;
        public string IdUsuario { get; set; } = null!;
        public ApplicationUser Usuario { get; set; } = null!;
        public DateTime Expiracion { get; set; }
    }
}
