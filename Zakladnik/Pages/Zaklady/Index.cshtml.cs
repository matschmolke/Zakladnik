using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
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

        //CSV Export
        public async Task<IActionResult> OnPostExportAsync()
        {
            var zaklady = await _context.Zaklady.ToListAsync();

            var csv = new StringBuilder();
            csv.AppendLine("Id;Data;Bukmacher;Stawka;Kurs;Wygrany;Podatek;EWK;Wygrana");

            foreach (var z in zaklady)
            {
                var line = string.Join(";", new[]
                {
                    z.Id.ToString(),
                    z.Data.ToString("yyyy-MM-dd HH:mm"),
                    z.Bukmacher,
                    z.Stawka.ToString("0.00", CultureInfo.InvariantCulture),
                    z.Kurs.ToString("0.00", CultureInfo.InvariantCulture),
                    z.Wygrany ? "Tak" : "Nie",
                    z.Podatek.ToString("0.00", CultureInfo.InvariantCulture),
                    z.PotencjalnaWygrana.ToString("0.00", CultureInfo.InvariantCulture),
                    z.FaktycznaWygrana.ToString("0.00", CultureInfo.InvariantCulture)
                });

                csv.AppendLine(line);
            }

            var bytes = Encoding.UTF8.GetBytes(csv.ToString());
            var fileName = $"zaklady_{DateTime.Now:yyyyMMdd}.csv";

            return File(bytes, "text/csv", fileName);
        }
    }
}
