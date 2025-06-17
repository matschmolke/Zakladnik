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

namespace Zakladnik.Pages.Zaklady
{
    public class CreateModel : PageModel
    {
        private readonly Zakladnik.Data.AppDbContext _context;
        public List<SelectListItem> Bukmacherzy { get; set; }


        public CreateModel(Zakladnik.Data.AppDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            Zaklad = new Zaklad
            {
                Data = new DateTime(
                    DateTime.Now.Year,
                    DateTime.Now.Month,
                    DateTime.Now.Day,
                    DateTime.Now.Hour,
                    DateTime.Now.Minute,
                    0),
                Podatek = 12,
                Rozliczony = true
            };

            return Page();
        }


        [BindProperty]
        public Zaklad Zaklad { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }



            _context.Zaklady.Add(Zaklad);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
