namespace Aviator.Main.Database;

public class AviatorRepository(IServiceScopeFactory scopeFactory)
{
    public AviatorContext AviatorContext =>
        scopeFactory.CreateScope().ServiceProvider.GetRequiredService<AviatorContext>();
}