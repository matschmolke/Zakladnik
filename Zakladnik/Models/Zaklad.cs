using System.ComponentModel.DataAnnotations;

namespace Zakladnik.Models
{
    public class Zaklad
    {
        public int Id { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-ddTHH:mm}", ApplyFormatInEditMode = true)]
        public DateTime Data { get; set; }

        [Required(ErrorMessage = "Podaj nazwę bukmachera.")]
        public string Bukmacher { get; set; }

        [Required(ErrorMessage = "Podaj stawkę.")]
        [Range(1, double.MaxValue, ErrorMessage = "Stawka musi wynosić minimum 1zł")]
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "Podaj liczbę z maksymalnie 2 miejscami po przecinku")]
        [Display(Name = "Stawka")]
        public decimal Stawka { get; set; }

        [Required(ErrorMessage = "Podaj kurs.")]
        [Range(1.01, 10000, ErrorMessage = "Kurs musi wynosić od 1.01 do 10000")]
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "Podaj liczbę z maksymalnie 2 miejscami po przecinku")]
        public decimal Kurs { get; set; }

        public bool Wygrany { get; set; }

        public bool Rozliczony { get; set; }

        [Range(0, 100, ErrorMessage = "Podatek musi wynosić od 0 do 100%")]
        [Display(Name = "Podatek (%)")]
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "Podaj liczbę z maksymalnie 2 miejscami po przecinku")]
        public decimal Podatek { get; set; } = 12;

        [Display(Name = "Potencjalna wygrana")]
        public decimal PotencjalnaWygrana =>
            Math.Round((Stawka - (Stawka * Podatek / 100)) * Kurs, 2);

        [Display(Name = "Faktyczna wygrana")]
        public decimal FaktycznaWygrana => Wygrany ? PotencjalnaWygrana : 0;
    }
}
