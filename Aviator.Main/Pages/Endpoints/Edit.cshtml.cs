using Aviator.Main.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Aviator.Main.Pages.Endpoints;

public class EditModel : PageModel
{
    private readonly AviatorContext _context;

    public EditModel(AviatorContext context)
    {
        _context = context;
    }

    [BindProperty] public Models.Endpoint Endpoint { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(Guid? id)
    {
        if (id == null) return NotFound();

        var endpoint = await _context.Endpoints.FirstOrDefaultAsync(m => m.Guid == id);
        if (endpoint == null) return NotFound();
        Endpoint = endpoint;
        return Page();
    }

    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see https://aka.ms/RazorPagesCRUD.
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid) return Page();

        _context.Attach(Endpoint).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!EndpointExists(Endpoint.Guid))
                return NotFound();
            throw;
        }

        return RedirectToPage("./Index");
    }

    private bool EndpointExists(Guid id)
    {
        return _context.Endpoints.Any(e => e.Guid == id);
    }
}