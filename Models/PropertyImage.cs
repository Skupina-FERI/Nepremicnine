namespace RZ_nepremicnine.Models
{
    public class PropertyImage
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public int NepremicninaId { get; set; }
        public Nepremicnina? Nepremicnina { get; set; }
    }
}
