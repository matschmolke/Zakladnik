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

        public decimal PotencjalnaWygrana => Math.Round(Stawka * Kurs, 2);
    }
}
