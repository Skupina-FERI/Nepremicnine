using Microsoft.AspNetCore.Identity;


namespace RZ_nepremicnine.Models
{
    public class Image : IdentityUser
    {
        public string ImageUrl { get; set; }
    }
}