using Aviator.Main.Handler;
using Aviator.Main.Models.Acars;
using Aviator.Main.Services;
using Serilog;

namespace Aviator.Main.Worker;

public class AviatorRouter : BackgroundService
{
    private readonly AviatorRouterService _routerService;

    public AviatorRouter(AviatorRouterService routerService)
    {
        _routerService = routerService;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _routerService.RouterInput.HandleMessage += RouterInputOnHandleMessage;

        await _routerService.RouterInput.Start(stoppingToken);

        await Task.Delay(1000, stoppingToken);
    }

    private static void RouterInputOnHandleMessage(byte[] buffer)
    {
        var byteHandler = new ByteHandler();
        var jsonHandler = new JsonHandler();
        var extractionHandler = new ExtractionHandler();

        var decoders = new List<IHandler>
        {
            new JaeroHandler(),
            new DumpVdl2Handler(),
            new AcarsdecHandler()
        };

        // Next handler is json
        byteHandler.NextHandler(jsonHandler);

        var json = byteHandler.Handle(buffer);

        if (json is null) return;

        //decoders.ForEach(d => d.NextHandler(extractionHandler));

        var t = decoders
            .Select(s => s.Handle(json))
            .Select(s => extractionHandler.Handle(s))
            .First(s => s is not null);

        if (t is not AcarsFrame frame) return;

        Log.Information("{Frame}", frame.Channel);

        var b = 1 + 1;
    }
}