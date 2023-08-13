using Sharee.Application.Data;
using Sharee.Application.Data.Entities;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Sharee.Application.Authorization;

namespace Sharee.Application.Pages;

[AuthorizationToken]
public class Index : PageModel
{
    private readonly ILogger<Index> _logger;
    private readonly ShareeDbContext _context;

    public Index(ShareeDbContext context, ILogger<Index> logger)
    {
        _logger = logger;
        _context = context;
    }
    
    public IList<Unit> Unit { get;set; } = default!;

    public async Task OnGetAsync()
    {
        this.Unit = await _context.Units.ToListAsync();
    }
}