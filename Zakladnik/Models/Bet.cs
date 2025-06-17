using System.ComponentModel.DataAnnotations;

namespace Zakladnik.Models
{
    public class Bet
    {
        public int Id { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-ddTHH:mm}", ApplyFormatInEditMode = true)]
        [Display(Name = "Data")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Podaj nazwę bukmachera.")]
        [Display(Name = "Bukmacher")]
        public string Bookmaker { get; set; }

        [Required(ErrorMessage = "Podaj stawkę.")]
        [Range(1, double.MaxValue, ErrorMessage = "Stawka musi wynosić minimum 1zł")]
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "Podaj liczbę z maksymalnie 2 miejscami po przecinku")]
        [Display(Name = "Stawka")]
        public decimal Stake { get; set; }

        [Required(ErrorMessage = "Podaj kurs.")]
        [Range(1.01, 10000, ErrorMessage = "Kurs musi wynosić od 1.01 do 10000")]
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "Podaj liczbę z maksymalnie 2 miejscami po przecinku")]
        [Display(Name = "Kurs")]
        public decimal Odds { get; set; }

        [Display(Name = "Wygrany")]
        public bool IsWon { get; set; }

        [Display(Name = "Rozliczony")]
        public bool IsSettled { get; set; }

        [Range(0, 100, ErrorMessage = "Podatek musi wynosić od 0 do 100%")]
        [Display(Name = "Podatek (%)")]
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "Podaj liczbę z maksymalnie 2 miejscami po przecinku")]
        public decimal Tax { get; set; } = 12;

        [Display(Name = "Potencjalna wygrana")]
        public decimal PotentialWinnings => Math.Round((Stake - (Stake * Tax / 100)) * Odds, 2);

        [Display(Name = "Faktyczna wygrana")]
        public decimal ActualWinnings => IsWon ? PotentialWinnings : 0;
    }
}
