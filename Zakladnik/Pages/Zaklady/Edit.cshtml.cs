using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Zakladnik.Data;
using Zakladnik.Models;

namespace Zakladnik.Pages.Zaklady
{
    public class EditModel : PageModel
    {
        private readonly Zakladnik.Data.AppDbContext _context;

        public EditModel(Zakladnik.Data.AppDbContext context)
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

            var zaklad =  await _context.Zaklady.FirstOrDefaultAsync(m => m.Id == id);
            if (zaklad == null)
            {
                return NotFound();
            }
            Zaklad = zaklad;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Zaklad).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ZakladExists(Zaklad.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ZakladExists(int id)
        {
            return _context.Zaklady.Any(e => e.Id == id);
        }
    }
}
