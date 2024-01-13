using Aviator.Main.Database;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Aviator.Main.Pages.Endpoint;

public class IndexModel : PageModel
{
    private readonly AviatorContext _context;

    public IndexModel(AviatorContext context)
    {
        _context = context;
    }

    public IList<Models.Endpoint> Endpoint { get; set; } = default!;

    public async Task OnGetAsync()
    {
        var grouped = _context.Outputs
            .AsEnumerable()
            .GroupBy(s => s.Decoder)
            .SelectMany(s => s);

        Endpoint = grouped.ToList();
    }
}