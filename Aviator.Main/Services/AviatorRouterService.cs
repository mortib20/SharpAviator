using System.Collections.Immutable;
using Aviator.Main.Models;
using Aviator.Main.Models.Router.Input;
using Aviator.Main.Models.Router.Output;

namespace Aviator.Main.Services;

public class AviatorRouterService(AviatorRouterOutputService outputService, ILoggerFactory loggerFactory)
{
    public IRouterInput RouterInput { get; private set; } = new UdpRouterInput(21000, loggerFactory);
    public readonly AviatorRouterOutputService OutputService = outputService;

    public void WriteToDecoderOutputs(Decoder decoder, byte[] buffer, CancellationToken cancellationToken)
    {
        OutputService.Outputs[decoder].ForEach(s => s.Write(buffer, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false));
    }
}