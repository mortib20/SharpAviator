using System.Net;
using System.Net.Sockets;

namespace Aviator.Main.Models.Router.Output;

public class TcpRouterOutput : IRouterOutput
{
    private readonly IPEndPoint _ipEndPoint;
    private readonly ILogger _logger;
    private bool _connecting;
    private bool _error;
    private TcpClient _tcpClient = new();
    public bool Connected => _tcpClient.Connected;

    public TcpRouterOutput(string address, int port, ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger($"TCP:{address}:{port}");
        _ipEndPoint = new IPEndPoint(Dns.GetHostAddresses(address).First(), port);
        _logger.LogInformation("Created");
    }


    public async Task WriteAsync(ReadOnlyMemory<byte> buffer, CancellationToken cancellationToken = default)
    {
        if (!_tcpClient.Connected && !_connecting)
        {
            Connect(cancellationToken);
            return;
        }

        if (_tcpClient.Connected)
        {
            _connecting = false;
            try
            {
                await _tcpClient.GetStream().WriteAsync(buffer, cancellationToken).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                _logger.LogError("{Error}", e.Message);
                _error = true;
            }
        }
    }

    public void Connect(CancellationToken cancellationToken = default)
    {
        _connecting = true;
        _logger.LogInformation("Connect");
        if (_error)
        {
            _error = false;
            _tcpClient = new TcpClient();
        }
        _tcpClient.Connect(_ipEndPoint);
    }
}