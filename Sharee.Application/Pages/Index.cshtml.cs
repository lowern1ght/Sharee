using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Sharee.Application.Pages;

public class Index : PageModel
{
    public async Task OnGetAsync()
    {
        await Task.CompletedTask;
    }
}