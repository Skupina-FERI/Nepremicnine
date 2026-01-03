using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RZ_nepremicnine.Models;
using System.Threading.Tasks;

namespace RZ_nepremicnine.Pages.Admin
{
    public class ManageModel : PageModel
    {
        private readonly UserManager<Uporabniki> _userManager;

        public ManageModel(UserManager<Uporabniki> userManager)
        {
            _userManager = userManager;
        }

        public string FullName { get; set; }
        public string Email { get; set; }

        public async Task OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user != null)
            {
                FullName = user.FullName;
                Email = user.Email;
            }
        }
    }
}
