using System.ComponentModel.DataAnnotations;

namespace Zakladnik.Models
{
    public class Zaklad
    {
        public int Id { get; set; }

        [Required]
        public DateTime Data { get; set; }

        [Required]
        public string Bukmacher { get; set; }

        [Required]
        public decimal Stawka { get; set; }

        [Required]
        [Range(1.01, 10000)]
        public decimal Kurs { get; set; }

        public bool Wygrany { get; set; }

        [Range(0, 100)]
        [Display(Name = "Podatek (%)")]
        public decimal Podatek { get; set; } = 12;

        [Display(Name = "Potencjalna wygrana")]
        public decimal PotencjalnaWygrana =>
            Math.Round((Stawka - (Stawka * Podatek / 100)) * Kurs, 2);

        [Display(Name = "Faktyczna wygrana")]
        public decimal FaktycznaWygrana => Wygrany ? PotencjalnaWygrana : 0;
    }
}
