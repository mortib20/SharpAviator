using System.Net;
using System.Net.Sockets;

namespace Aviator.Main.Models.Router.Output;

public class TcpRouterOutput : IRouterOutput
{
    private TcpClient _tcpClient = new();
    private readonly IPEndPoint _ipEndPoint;
    private readonly ILogger _logger;

    public TcpRouterOutput(string address, int port, ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger($"{address}:{port}");
        _ipEndPoint = new IPEndPoint(Dns.GetHostAddresses(address).First(), port);
        _logger.LogInformation("Created");
    }

    public async Task Connect(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Connect");
        _tcpClient = new TcpClient();
        await _tcpClient.ConnectAsync(_ipEndPoint, cancellationToken).ConfigureAwait(false);
    }

    public async ValueTask Write(ReadOnlyMemory<byte> buffer, CancellationToken cancellationToken = default)
    {
        if (!_tcpClient.Connected)
        {
            await Connect(cancellationToken);
        }
        
        if (_tcpClient.Connected)
        {
            try
            {
                await _tcpClient.GetStream().WriteAsync(buffer, cancellationToken).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}