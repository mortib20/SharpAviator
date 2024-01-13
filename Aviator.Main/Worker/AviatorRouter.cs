namespace Aviator.Main.Worker;

public class AviatorRouter : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await Task.Delay(1000, stoppingToken);
    }
}