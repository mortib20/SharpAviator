using Aviator.Main.Database;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Endpoint = Aviator.Main.Models.Endpoint;

namespace Aviator.Main.Pages.Endpoints;

public class IndexModel : PageModel
{
    private readonly AviatorContext _context;

    public IndexModel(AviatorContext context)
    {
        _context = context;
    }

    public IList<Endpoint> Endpoint { get; set; } = default!;

    public async Task OnGetAsync()
    {
        var grouped = _context.Endpoints
            .AsEnumerable()
            .GroupBy(s => s.Decoder)
            .SelectMany(s => s);

        Endpoint = grouped.ToList();
    }
}