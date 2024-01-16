using System.Net;
using System.Net.Sockets;
using Serilog;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace Aviator.Main.Models.Router.Output;

public class TcpRouterOutput : IRouterOutput
{
    private readonly IPEndPoint _ipEndPoint;
    private readonly ILogger _logger;
    private Timer _timer;

    private async void CheckConnection(object? state)
    {
        if (_tcpClient.Connected || _connecting) return;
        
        await Connect();
    }

    private bool _connecting;
    private int _timeout = 1000;
    private bool _error;
    private TcpClient _tcpClient = new();
    public bool Connected => _tcpClient.Connected;

    
    
    public TcpRouterOutput(string address, int port, ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger($"TCP:{address}:{port}");
        _ipEndPoint = new IPEndPoint(Dns.GetHostAddresses(address).First(), port);
        _logger.LogInformation("Created");
        _timer = new Timer(CheckConnection, null, TimeSpan.FromSeconds(2), TimeSpan.FromSeconds(10));
    }


    public async Task WriteAsync(ReadOnlyMemory<byte> buffer, CancellationToken cancellationToken = default)
    {
        if (_tcpClient.Connected)
        {
            _timeout = 1000;
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

    public async Task Connect(CancellationToken cancellationToken = default)
    {
        _connecting = true;
        _logger.LogInformation("Connect");
        if (_error)
        {
            _error = false;
            _tcpClient = new TcpClient();
        }

        try
        {
            await _tcpClient.ConnectAsync(_ipEndPoint, cancellationToken).ConfigureAwait(false);
        }
        catch (Exception)
        {
            _logger.LogWarning("Failed to connect");
            _error = true;
        }

        if (_tcpClient.Connected)
        {
            _logger.LogInformation("Connected");
        }
        _connecting = false;
    }
}