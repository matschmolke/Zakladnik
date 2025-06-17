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

namespace Zakladnik.Pages.Bets
{
    public class IndexModel : PageModel
    {
        private readonly Zakladnik.Data.AppDbContext _context;
        public decimal Balance { get; set; }
        public string? FilterBookmaker { get; set; }
        public string? FilterStatus { get; set; }

        public IndexModel(Zakladnik.Data.AppDbContext context)
        {
            _context = context;
        }

        public IList<Bet> Bet { get;set; } = default!;

        public async Task OnGetAsync(string? filterBookmaker, string? filterStatus)
        {
            this.FilterBookmaker = filterBookmaker;
            this.FilterStatus = filterStatus;

            var query = _context.Bets.AsQueryable();

            if (!string.IsNullOrWhiteSpace(this.FilterBookmaker))
            {
                query = query.Where(z => z.Bookmaker.Contains(FilterBookmaker));
            }

            if (!string.IsNullOrWhiteSpace(FilterStatus))
            {
                if (this.FilterStatus == "Won")
                    query = query.Where(z => z.IsSettled && z.IsWon);
                else if (this.FilterStatus == "Lost")
                    query = query.Where(z => z.IsSettled && !z.IsWon);
                else if (this.FilterStatus == "Unsettled")
                    query = query.Where(z => !z.IsSettled);
            }

            this.Bet = await query.ToListAsync();
            this.Balance = Bet.Sum(z => z.ActualWinnings - z.Stake);
        }

        //CSV Export
        public async Task<IActionResult> OnPostExportAsync()
        {
            var bets = await _context.Bets.ToListAsync();

            var csv = new StringBuilder();
            csv.AppendLine("Id;Data;Bukmacher;Stawka;Kurs;Wygrany;Podatek;EWK;Wygrana");

            foreach (var z in bets)
            {
                var line = string.Join(";", new[]
                {
                    z.Id.ToString(),
                    z.Date.ToString("yyyy-MM-dd HH:mm"),
                    z.Bookmaker,
                    z.Stake.ToString("0.00", CultureInfo.InvariantCulture),
                    z.Odds.ToString("0.00", CultureInfo.InvariantCulture),
                    z.IsWon ? "Tak" : "Nie",
                    z.Tax.ToString("0.00", CultureInfo.InvariantCulture),
                    z.PotentialWinnings.ToString("0.00", CultureInfo.InvariantCulture),
                    z.ActualWinnings.ToString("0.00", CultureInfo.InvariantCulture)
                });

                csv.AppendLine(line);
            }

            var bytes = Encoding.UTF8.GetBytes(csv.ToString());
            var fileName = $"zakladnik_{DateTime.Now:yyyyMMdd}.csv";

            return File(bytes, "text/csv", fileName);
        }
    }
}
