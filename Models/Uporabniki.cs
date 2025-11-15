using Microsoft.AspNetCore.Identity;

namespace RZ_nepremicnine.Models
{
    public class Uporabniki : IdentityUser
    {
        public string FullName { get; set; }
    }
}