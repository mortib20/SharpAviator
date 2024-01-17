using Aviator.Main.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Endpoint = Aviator.Main.Models.Endpoint;

namespace Aviator.Main.Pages.Endpoints;

public class DetailsModel : PageModel
{
    private readonly AviatorContext _context;

    public DetailsModel(AviatorContext context)
    {
        _context = context;
    }

    public Endpoint Endpoint { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(Guid? id)
    {
        if (id == null) return NotFound();

        var endpoint = await _context.Endpoints.FirstOrDefaultAsync(m => m.Guid == id);
        if (endpoint == null)
            return NotFound();
        Endpoint = endpoint;
        return Page();
    }
}