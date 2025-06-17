using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Zakladnik.Data;
using Zakladnik.Models;

namespace Zakladnik.Pages.Bets
{
    public class CreateModel : PageModel
    {
        private readonly Zakladnik.Data.AppDbContext _context;

        public CreateModel(Zakladnik.Data.AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Bet Bet { get; set; } = default!;

        public IActionResult OnGet()
        {
            this.Bet = new Bet
            {
                Date = new DateTime(
                    DateTime.Now.Year,
                    DateTime.Now.Month,
                    DateTime.Now.Day,
                    DateTime.Now.Hour,
                    DateTime.Now.Minute,
                    0),
                Tax = 12,
                IsSettled = true
            };

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Bets.Add(this.Bet);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
