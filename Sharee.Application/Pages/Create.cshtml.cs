using Sharee.Application.Data;
using Microsoft.AspNetCore.Mvc;
using Sharee.Application.Data.Entities;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sharee.Application.Authorization;

namespace Sharee.Application.Pages;

[AuthorizationToken]
public class CreateModel : PageModel
{
    private readonly ShareeDbContext _context;

    public CreateModel(ShareeDbContext context)
    {
        _context = context;
    }

    public IActionResult OnGet()
    {
        return Page();
    }

    [BindProperty]
    public Unit Unit { get; set; } = default!;
        
    public async Task<IActionResult> OnPostAsync()
    {
          
        if (!ModelState.IsValid) 
        { 
            return Page(); 
        }

        _context.Units.Add(Unit);
        await _context.SaveChangesAsync();
            
        return RedirectToPage("./Index");
    }
}