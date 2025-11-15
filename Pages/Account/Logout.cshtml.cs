using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using RZ_nepremicnine.Models;

namespace RZ_nepremicnine.Pages.Account
{
    public class Logout : PageModel
    {
        private readonly SignInManager<Uporabniki> _signInManager;

        public Logout(SignInManager<Uporabniki> signInManager)
        {
            _signInManager = signInManager;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _signInManager.SignOutAsync();
            return RedirectToPage("/Index");
        }
    }
}
