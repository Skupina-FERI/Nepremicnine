using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using RZ_nepremicnine.Data;
using RZ_nepremicnine.Models;

namespace RZ_nepremicnine.Pages.Properties
{
    public class BrowseModel : PageModel
    {
        private readonly ILogger<BrowseModel> _logger;
        private readonly AppDbContext _context;

        public BrowseModel(ILogger<BrowseModel> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }
    
            // TODO: Load available properties for browsing
            public List<Nepremicnina> Nepreminine { get; set; }

        public async Task OnGetAsync()
        {
            Nepreminine = await GetAllNepremicnineAsync();
        }

        //Simple dobi vse nepremicnine
        public async Task<List<Nepremicnina>> GetAllNepremicnineAsync()
        {
            return await _context.Nepremicnine.ToListAsync();
        }

                    
    }
}
