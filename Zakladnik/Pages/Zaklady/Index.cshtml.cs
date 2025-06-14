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

        public IndexModel(Zakladnik.Data.AppDbContext context)
        {
            _context = context;
        }

        public IList<Zaklad> Zaklad { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Zaklad = await _context.Zaklady.ToListAsync();
        }
    }
}
