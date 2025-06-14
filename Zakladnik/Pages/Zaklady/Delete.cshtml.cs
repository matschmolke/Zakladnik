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
    public class DeleteModel : PageModel
    {
        private readonly Zakladnik.Data.AppDbContext _context;

        public DeleteModel(Zakladnik.Data.AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Zaklad Zaklad { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zaklad = await _context.Zaklady.FirstOrDefaultAsync(m => m.Id == id);

            if (zaklad is not null)
            {
                Zaklad = zaklad;

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

            var zaklad = await _context.Zaklady.FindAsync(id);
            if (zaklad != null)
            {
                Zaklad = zaklad;
                _context.Zaklady.Remove(Zaklad);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
