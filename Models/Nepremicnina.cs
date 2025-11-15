/*using System.ComponentModel.DataAnnotations;

namespace RZ_nepremicnine.Models
{
    public class Nepremicnina
    {
        public int id { get; set; }

        [Required]
        [StringLength(100)]
        public string Naziv { get; set; }
        public string? Description { get; set; }

        [Required]
        public decimal Cena { get; set; }

        [StringLength(50)]
        public string? Mesto { get; set; }

        public string? Naslov { get; set; }

        public int? Spalnic { get; set; }
        public int? Kopalnic { get; set; }
        public decimal? Kvadratura { get; set; }
        
        public string Status { get; set; } = "Na voljo";
        
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}*/