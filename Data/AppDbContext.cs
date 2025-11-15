using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RZ_nepremicnine.Models;

namespace RZ_nepremicnine.Data
{
    public class AppDbContext : IdentityDbContext<Uporabniki>
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
            
        }
    }
}