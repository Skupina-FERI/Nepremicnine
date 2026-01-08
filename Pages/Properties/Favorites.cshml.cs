using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore; // ⬅ NUJNO
using RZ_nepremicnine.Data;
using RZ_nepremicnine.Models;

namespace RZ_nepremicnine.Pages.Properties
{
    public class FavoritesModel : PageModel
    {
        private readonly AppDbContext _context;

        public FavoritesModel(AppDbContext context)
        {
            _context = context;
        }

        public List<Nepremicnina> Favorites { get; set; } = new();

        public void OnGet()
        {
            var favs = HttpContext.Session.GetString("Favorites");

            if (!string.IsNullOrEmpty(favs))
            {
                var ids = favs.Split(',').Select(int.Parse).ToList();

                Favorites = _context.Nepremicnine
                    .Include(n => n.Images)
                    .Where(n => ids.Contains(n.Id))
                    .ToList();
            }
        }
    }
}
