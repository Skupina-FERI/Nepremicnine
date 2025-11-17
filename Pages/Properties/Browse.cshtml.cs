using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RZ_nepremicnine.Pages.Properties
{
    public class BrowseModel : PageModel
    {
        private readonly ILogger<BrowseModel> _logger;

        public BrowseModel(ILogger<BrowseModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            // TODO: Load available properties for browsing
        }
    }
}
