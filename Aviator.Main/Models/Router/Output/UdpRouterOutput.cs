using System.Net;
using System.Net.Sockets;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace Aviator.Main.Models.Router.Output;

public class UdpRouterOutput : IRouterOutput
{
    private readonly IPEndPoint _ipEndPoint;
    private readonly ILogger _logger;
    private readonly UdpClient _udpClient = new();

    public UdpRouterOutput(string address, int port, ILoggerFactory loggerFactory)
    {
        _ipEndPoint = new IPEndPoint(Dns.GetHostAddresses(address).First(), port);
        _logger = loggerFactory.CreateLogger($"UDP:{address}:{port}");
        _logger.LogInformation("Created");
    }

    public bool Connected => true;

    public async Task WriteAsync(ReadOnlyMemory<byte> buffer, CancellationToken cancellationToken = default)
    {
        try
        {
            await _udpClient.SendAsync(buffer, _ipEndPoint, cancellationToken).ConfigureAwait(false);
        }
        catch (Exception e)
        {
            _logger.LogError("{Message}", e.Message);
        }
    }
}