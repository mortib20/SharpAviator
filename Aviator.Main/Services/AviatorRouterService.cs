using Aviator.Main.Models;
using Aviator.Main.Models.Router.Input;

namespace Aviator.Main.Services;

public class AviatorRouterService(AviatorRouterOutputService outputService, ILoggerFactory loggerFactory)
{
    public readonly AviatorRouterOutputService OutputService = outputService;
    public IRouterInput RouterInput { get; private set; } = new UdpRouterInput(21000, loggerFactory);

    public void WriteToDecoderOutputs(Decoder decoder, byte[] buffer, CancellationToken cancellationToken)
    {
        OutputService.Outputs[decoder].ForEach(s =>
            s.Write(buffer, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false));
    }
}