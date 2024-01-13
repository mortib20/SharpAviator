using System.Net.Sockets;
using Serilog;

namespace Aviator.Main.Models.Router;

public sealed class UdpRouterInput(int port) : IRouterInput
{
    private readonly UdpClient _udpClient = new UdpClient(port);
    public event HandleMessage? HandleMessage;

    public async Task Start(CancellationToken cancellationToken = default)
    {
        Log.Information("Start listening on {EndPoint}", _udpClient.Client.LocalEndPoint);
        
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