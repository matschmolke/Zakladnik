using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Zakladnik.Data;

namespace Zakladnik.Pages
{
    public class ResetModel : PageModel
    {
        private readonly AppDbContext _context;

        public ResetModel(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            _context.Zaklady.RemoveRange(_context.Zaklady);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Index");
        }
    }
}
