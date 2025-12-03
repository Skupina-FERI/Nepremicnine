using System.ComponentModel.DataAnnotations;

namespace RZ_nepremicnine.Models
{
    public class Nepremicnina
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string? Naziv { get; set; }

        public string? Description { get; set; }

        [Required]
        public decimal Cena { get; set; }

        // OLD version (must keep)
        [StringLength(50)]
        public string? Regija { get; set; }

        [StringLength(50)]
        public string? Mesto { get; set; }

        public string? Naslov { get; set; }

        [StringLength(50)]
        public string? TipNepremicnine { get; set; }

        public int? Spalnic { get; set; }
        public int? Kopalnic { get; set; }
        public decimal? Kvadratura { get; set; }

        public string Status { get; set; } = "Na voljo";
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Relationships
        public string? UporabnikiId { get; set; }
        public Uporabniki? Owner { get; set; }

        public ICollection<PropertyImage>? Images { get; set; }

        public int? RegijaFK { get; set; }
        public Regija? RegijaNavigation { get; set; }

        public int? PosredovanjeFK { get; set; }
        public Posredovanje? PosredovanjeNavigation { get; set; }

        public int? VrstaNepremicnineFK { get; set; }
        public VrstaNepremicnine? VrstaNepremicnineNavigation { get; set; }
    }

}
