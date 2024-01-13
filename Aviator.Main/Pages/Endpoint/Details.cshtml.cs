using Aviator.Main.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Aviator.Main.Pages.Endpoint;

public class DetailsModel : PageModel
{
    private readonly AviatorContext _context;

    public DetailsModel(AviatorContext context)
    {
        _context = context;
    }

    public Models.Endpoint Endpoint { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(Guid? id)
    {
        if (id == null) return NotFound();

        var endpoint = await _context.Outputs.FirstOrDefaultAsync(m => m.Guid == id);
        if (endpoint == null)
            return NotFound();
        Endpoint = endpoint;
        return Page();
    }
}