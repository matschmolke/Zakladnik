using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Zakladnik.Data;
using Zakladnik.Models;

namespace Zakladnik.Pages.Bets
{
    public class DeleteModel : PageModel
    {
        private readonly Zakladnik.Data.AppDbContext _context;

        public DeleteModel(Zakladnik.Data.AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Bet Bet { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bet = await _context.Bets.FirstOrDefaultAsync(m => m.Id == id);

            if (bet is not null)
            {
                this.Bet = bet;

                return Page();
            }

            return NotFound();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bet = await _context.Bets.FindAsync(id);
            if (bet != null)
            {
                this.Bet = bet;
                _context.Bets.Remove(this.Bet);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
