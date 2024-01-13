using Aviator.Main.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Aviator.Main.Pages.Endpoint;

public class CreateModel : PageModel
{
    private readonly AviatorContext _context;

    public CreateModel(AviatorContext context)
    {
        _context = context;
    }

    [BindProperty] public Models.Endpoint Endpoint { get; set; } = default!;

    public IActionResult OnGet()
    {
        return Page();
    }

    // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid) return Page();

        _context.Outputs.Add(Endpoint);
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}