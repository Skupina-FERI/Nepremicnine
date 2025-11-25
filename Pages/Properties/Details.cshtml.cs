using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RZ_nepremicnine.Data;
using RZ_nepremicnine.Models;

namespace RZ_nepremicnine.Pages.Properties
{
    public class DetailsModel : PageModel
    {
        private readonly AppDbContext _context;

        public DetailsModel(AppDbContext context)
        {
            _context = context;
        }

        public Nepremicnina Nepremicnina { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Nepremicnina = await _context.Nepremicnine
                .Include(n => n.Owner)
                .Include(n => n.Images)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (Nepremicnina == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}
