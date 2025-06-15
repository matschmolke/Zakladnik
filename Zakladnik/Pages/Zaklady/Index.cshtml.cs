using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Zakladnik.Data;
using Zakladnik.Models;

namespace Zakladnik.Pages.Zaklady
{
    public class IndexModel : PageModel
    {
        private readonly Zakladnik.Data.AppDbContext _context;
        public decimal Bilans { get; set; }
        public string? FiltrBukmacher { get; set; }
        public string? FiltrStatus { get; set; }

        public IndexModel(Zakladnik.Data.AppDbContext context)
        {
            _context = context;
        }

        public IList<Zaklad> Zaklad { get;set; } = default!;

        public async Task OnGetAsync(string? filtrBukmacher, string? filtrStatus)
        {
            FiltrBukmacher = filtrBukmacher;
            FiltrStatus = filtrStatus;

            var query = _context.Zaklady.AsQueryable();

            if (!string.IsNullOrWhiteSpace(FiltrBukmacher))
            {
                query = query.Where(z => z.Bukmacher.Contains(FiltrBukmacher));
            }

            if (!string.IsNullOrWhiteSpace(FiltrStatus))
            {
                if (FiltrStatus == "Wygrany")
                    query = query.Where(z => z.Rozliczony && z.Wygrany);
                else if (FiltrStatus == "Przegrany")
                    query = query.Where(z => z.Rozliczony && !z.Wygrany);
                else if (FiltrStatus == "Nierozliczony")
                    query = query.Where(z => !z.Rozliczony);
            }

            Zaklad = await query.ToListAsync();
            Bilans = Zaklad.Sum(z => z.FaktycznaWygrana - z.Stawka);
        }
    }
}
