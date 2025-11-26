using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RZ_nepremicnine.Data;
using RZ_nepremicnine.Models;
using System.ComponentModel.DataAnnotations;

namespace RZ_nepremicnine.Pages.Properties
{
    [Authorize]
    public class AddModel : PageModel
    {
        private readonly AppDbContext _context;
        private readonly UserManager<Uporabniki> _userManager;
        private readonly IWebHostEnvironment _environment;

        public AddModel(AppDbContext context, UserManager<Uporabniki> userManager, IWebHostEnvironment environment)
        {
            _context = context;
            _userManager = userManager;
            _environment = environment;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "Naziv je obvezen.")]
            [StringLength(100, ErrorMessage = "Naziv mora biti krajši od 100 znakov.")]
            public string Naziv { get; set; }

            public string? Description { get; set; }

            [Required(ErrorMessage = "Cena je obvezna.")]
            [Range(0, double.MaxValue, ErrorMessage = "Cena mora biti večja od 0.")]
            public decimal Cena { get; set; }

            [StringLength(50)]
            public string? Regija { get; set; }

            [StringLength(50)]
            public string? Mesto { get; set; }

            public string? Naslov { get; set; }

            [Required(ErrorMessage = "Tip nepremičnine je obvezen.")]
            public string TipNepremicnine { get; set; }

            [Range(0, double.MaxValue, ErrorMessage = "Kvadratura mora biti večja od 0.")]
            public decimal? Kvadratura { get; set; }

            [Range(0, int.MaxValue, ErrorMessage = "Število spalnic ne more biti negativno.")]
            public int? Spalnic { get; set; }

            [Range(0, int.MaxValue, ErrorMessage = "Število kopalnic ne more biti negativno.")]
            public int? Kopalnic { get; set; }

            public List<IFormFile>? Images { get; set; }
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToPage("/Account/Login");
            }

            var nepremicnina = new Nepremicnina
            {
                Naziv = Input.Naziv,
                Description = Input.Description,
                Cena = Input.Cena,
                Regija = Input.Regija,
                Mesto = Input.Mesto,
                Naslov = Input.Naslov,
                TipNepremicnine = Input.TipNepremicnine,
                Spalnic = Input.Spalnic,
                Kopalnic = Input.Kopalnic,
                Kvadratura = Input.Kvadratura,
                UporabnikiId = user.Id,
                CreatedAt = DateTime.Now,
                Status = "Na voljo"
            };

            _context.Nepremicnine.Add(nepremicnina);
            await _context.SaveChangesAsync();

            // Handle image uploads and save to PropertyImage table
            if (Input.Images != null && Input.Images.Count > 0)
            {
                // Ensure the directory exists
                var uploadsFolder = Path.Combine(_environment.WebRootPath, "images", "properties");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                foreach (var image in Input.Images)
                {
                    if (image.Length > 0)
                    {
                        // Generate unique filename to avoid conflicts
                        var fileExtension = Path.GetExtension(image.FileName);
                        var uniqueFileName = $"{Guid.NewGuid()}{fileExtension}";
                        var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                        // Save the file to disk
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await image.CopyToAsync(stream);
                        }

                        // Create PropertyImage record in database
                        _context.PropertyImages.Add(new PropertyImage
                        {
                            ImageUrl = $"/images/properties/{uniqueFileName}",
                            NepremicninaId = nepremicnina.Id
                        });
                    }
                }
                await _context.SaveChangesAsync();
            }

            TempData["SuccessMessage"] = "Oglas je bil uspešno ustvarjen!";
            return RedirectToPage("/Index");
        }
    }
}
