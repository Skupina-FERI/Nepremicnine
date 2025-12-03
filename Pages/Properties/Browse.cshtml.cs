using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RZ_nepremicnine.Data;
using RZ_nepremicnine.Models;

namespace RZ_nepremicnine.Pages.Properties
{
    public class BrowseModel : PageModel
    {
        public List<string> Region { get; set; } = new List<string>
        {
            "Pomurska", "Podravska", "Koroška", "Savinjska", "Zasavska", "Posavska",
            "Jugovzhodna Slovenija", "Osrednjeslovenska", "Gorenjska",
            "Primorsko-notranjska", "Goriška", "Obalno-kraška"
        };

        public List<string> Posredovanje { get; set; } = new List<string>
        {
            "Prodaja", "Oddaja", "Nakup", "Najem"
        };

        public List<string> VrstaNepremicnine { get; set; } = new List<string>
        {
            "Stanovanje", "Hiša", "Parcela", "Poslovni prostor",
            "Garaža", "Počitniški objekt", "Kmetijsko zemljišče"
        };

        // Filter binds
        [BindProperty(SupportsGet = true)]
        public string? SelectedRegion { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? SelectedPosredovanje { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? SelectedVrstaNepremicnine { get; set; }

        private readonly ILogger<BrowseModel> _logger;
        private readonly AppDbContext _context;

        public BrowseModel(ILogger<BrowseModel> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public List<Nepremicnina> Nepremicnine { get; set; }

        public async Task OnGetAsync()
        {
            var query = _context.Nepremicnine
                .Include(n => n.Images)
                .Include(n => n.PosredovanjeNavigation)
                .Include(n => n.VrstaNepremicnineNavigation)
                .AsQueryable();

            if (!string.IsNullOrEmpty(SelectedRegion))
            {
                query = query.Where(n => n.Regija == SelectedRegion);
            }

            if (!string.IsNullOrEmpty(SelectedPosredovanje))
            {
                query = query.Where(n => n.PosredovanjeNavigation != null && n.PosredovanjeNavigation.Name == SelectedPosredovanje);
            }

            if (!string.IsNullOrEmpty(SelectedVrstaNepremicnine))
            {
                query = query.Where(n => n.TipNepremicnine == SelectedVrstaNepremicnine);
            }

            Nepremicnine = await query.ToListAsync();
        }
    }
}
