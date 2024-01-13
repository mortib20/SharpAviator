using Aviator.Main.Database;
using Aviator.Main.Models.Router;

namespace Aviator.Main.Services;

public class AviatorRouterService(IServiceScopeFactory scopeFactory)
{
    private readonly AviatorContext _aviatorContext = scopeFactory.CreateScope().ServiceProvider.GetRequiredService<AviatorContext>();

    public IRouterInput RouterInput { get; } = new UdpRouterInput(21000);
}