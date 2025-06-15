using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Zakladnik.Data;
using Zakladnik.Models;

namespace Zakladnik.Pages;

public class IndexModel : PageModel
{
    private readonly AppDbContext _context;

    public IndexModel(AppDbContext context)
    {
        _context = context;
    }

    public int LiczbaZakladow { get; set; }
    public decimal LacznaStawka { get; set; }
    public decimal LacznaWygrana { get; set; }
    public decimal Bilans { get; set; }
    public double Skutecznosc { get; set; }

    public async Task OnGetAsync()
    {
        var zaklady = await _context.Zaklady.ToListAsync();

        LiczbaZakladow = zaklady.Count;
        LacznaStawka = zaklady.Sum(z => z.Stawka);
        LacznaWygrana = zaklady.Sum(z => z.FaktycznaWygrana);
        Bilans = LacznaWygrana - LacznaStawka;

        int wygrane = zaklady.Count(z => z.Wygrany);
        Skutecznosc = LiczbaZakladow > 0 ? Math.Round((double)wygrane / LiczbaZakladow * 100, 2) : 0;
    }
}
