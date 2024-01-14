using System.Net.Sockets;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace Aviator.Main.Models.Router.Input;

public sealed class UdpRouterInput(int port, ILoggerFactory loggerFactory) : IRouterInput
{
    private readonly ILogger _logger = loggerFactory.CreateLogger(nameof(UdpRouterInput));
    private readonly UdpClient _udpClient = new(port);
    public event HandleMessage? HandleMessage;

    public async Task Start(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Start listening on {EndPoint}", _udpClient.Client.LocalEndPoint);

        while (!cancellationToken.IsCancellationRequested)
        {
            var receiveResult = await _udpClient.ReceiveAsync(cancellationToken).ConfigureAwait(false);
            OnHandleMessage(receiveResult.Buffer);
        }
    }

    public void OnHandleMessage(byte[] buffer)
    {
        HandleMessage?.Invoke(buffer);
    }
}