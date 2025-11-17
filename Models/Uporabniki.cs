using Microsoft.AspNetCore.Identity;

namespace RZ_nepremicnine.Models
{
    public class Uporabniki : IdentityUser
    {
        public string FullName { get; set; }

        // Navigation property for owned properties
        public ICollection<Nepremicnina>? OwnedProperties { get; set; }
    }
}