using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Zakladnik.Data;
using Zakladnik.Models;

namespace Zakladnik.Pages;

public class IndexModel : PageModel
{
    private readonly AppDbContext _context;

    public IndexModel(AppDbContext context)
    {
        _context = context;
    }

    public int NumberOfBets { get; set; }
    public decimal TotalStake { get; set; }
    public decimal TotalWinnings { get; set; }
    public decimal Balance { get; set; }
    public double Accuracy { get; set; }

    public async Task OnGetAsync()
    {
        var bets = await _context.Bets.ToListAsync();

        NumberOfBets = bets.Count;
        TotalStake = bets.Sum(z => z.Stake);
        TotalWinnings = bets.Sum(z => z.ActualWinnings);
        Balance = TotalWinnings - TotalStake;

        int winnings = bets.Count(z => z.IsWon);
        Accuracy = NumberOfBets > 0 ? Math.Round((double)winnings / NumberOfBets * 100, 2) : 0;
    }
}
