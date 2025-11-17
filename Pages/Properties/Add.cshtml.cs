using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RZ_nepremicnine.Pages.Properties
{
    public class AddModel : PageModel
    {
        public void OnGet()
        {
        }

        public IActionResult OnPost(string NepremicninaNaziv)
        {
            // TODO: Implement property creation logic
            return Page();
        }
    }
}
