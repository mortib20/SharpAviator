using System.Runtime.CompilerServices;

namespace Aviator.Main.Models.Router.Output;

public class AviatorRouterOutput(Endpoint endpoint, ILoggerFactory loggerFactory)
{
    private readonly IRouterOutput _output = CreateOutput(endpoint, loggerFactory);
    public readonly Endpoint Endpoint = endpoint;
    public bool Started { get; private set; }
    public bool Connected => _output.Connected;

    public void Start()
    {
        if (Started) return;
        Started = true;
    }

    public void Stop()
    {
        if(!Started) return;
        Started = false;
    }

    public void Restart()
    {
        if (Started)
            Stop();
        if (!Started)
            Start();
    }
    
    public async Task Write(byte[] buffer, CancellationToken cancellationToken)
    {
        await _output.WriteAsync(buffer, cancellationToken);
    }

    private static IRouterOutput CreateOutput(Endpoint endpoint, ILoggerFactory loggerFactory)
    {
        return endpoint.Protocol switch
        {
            Protocol.Tcp => new TcpRouterOutput(endpoint.Address, endpoint.Port, loggerFactory),
            Protocol.Udp => new UdpRouterOutput(endpoint.Address, endpoint.Port, loggerFactory),
            _ => throw new SwitchExpressionException("Protocol not supported.")
        };
    }
}