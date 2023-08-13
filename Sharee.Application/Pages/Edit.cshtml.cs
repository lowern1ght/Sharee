using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Sharee.Application.Authorization;
using Sharee.Application.Data;
using Sharee.Application.Data.Entities;

namespace Sharee.Application.Pages;

[AuthorizationToken]
public class EditModel : PageModel
{
    private readonly ShareeDbContext _context;

    public EditModel(ShareeDbContext context)
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

        var unit =  await _context.Units.FirstOrDefaultAsync(m => m.Id == id);
        if (unit == null)
        {
            return NotFound();
        }
            
        Unit = unit;
            
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        _context.Attach(Unit).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!UnitExists(Unit.Id))
            {
                return NotFound();
            }

            throw;
        }

        return RedirectToPage("./Index");
    }

    private bool UnitExists(int id)
    {
        return (_context.Units?.Any(e => e.Id == id)).GetValueOrDefault();
    }
}