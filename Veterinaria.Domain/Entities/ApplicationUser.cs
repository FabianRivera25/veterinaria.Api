
using Microsoft.AspNetCore.Identity;

namespace Veterinaria.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string NombreCompleto { get; set; } = null!;
    }
}
