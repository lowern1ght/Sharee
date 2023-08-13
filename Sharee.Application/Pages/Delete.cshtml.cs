using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Sharee.Application.Authorization;
using Sharee.Application.Data;
using Sharee.Application.Data.Entities;

namespace Sharee.Application.Pages;

[AuthorizationToken]
public class DeleteModel : PageModel
{
    private readonly ShareeDbContext _context;

    public DeleteModel(ShareeDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Unit Unit { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var unit = await _context.Units.FirstOrDefaultAsync(m => m.Id == id);

        if (unit == null)
        {
            return NotFound();
        }

        this.Unit = unit;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        var unit = await _context.Units.FindAsync(id);

        if (unit != null)
        {
            Unit = unit;
            _context.Units.Remove(Unit);
            await _context.SaveChangesAsync();
        }

        return RedirectToPage("./Index");
    }
}