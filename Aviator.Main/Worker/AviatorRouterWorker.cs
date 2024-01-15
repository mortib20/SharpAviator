using Aviator.Main.Handler;
using Aviator.Main.Models;
using Aviator.Main.Services;

namespace Aviator.Main.Worker;

public class AviatorRouterWorker : BackgroundService
{
    private readonly ILogger _logger;
    private readonly AviatorRouterService _routerService;

    public AviatorRouterWorker(AviatorRouterService routerService, ILogger<AviatorRouterWorker> logger)
    {
        _routerService = routerService;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _routerService.RouterInput.HandleMessage += RouterInputOnHandleMessage;

        await _routerService.RouterInput.Start(stoppingToken);

        await Task.Delay(1000, stoppingToken);
    }

    private async Task RouterInputOnHandleMessage(byte[] buffer, CancellationToken cancellationToken = default)
    {
        var minimizeAcarsHandler = new MinimizeAcars();

        if (buffer.Length < 100) return;

        var jsonNode = JsonHelper.Parse(buffer);
        if (jsonNode is null) return;

        var decoder = DecoderDetector.Detect(jsonNode);
        if (decoder is null) return;

        _routerService.WriteToDecoderOutputs((Decoder)decoder, buffer, cancellationToken);
    }
}