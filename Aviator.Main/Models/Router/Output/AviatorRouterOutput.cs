using System.Runtime.CompilerServices;

namespace Aviator.Main.Models.Router.Output;

public class AviatorRouterOutput(Endpoint endpoint, ILoggerFactory loggerFactory)
{
    private readonly IRouterOutput _output = CreateOutput(endpoint, loggerFactory);
    public readonly Endpoint Endpoint = endpoint;
    public bool Writing { get; private set; }
    public DateTime LastMessage { get; private set; }
    public bool Connected => _output.Connected;

    public void Start()
    {
        if (Writing) return;
        Writing = true;
    }

    public void Stop()
    {
        if(!Writing) return;
        Writing = false;
    }

    public void Restart()
    {
        if (Writing)
            Stop();
        if (!Writing)
            Start();
    }
    
    public async Task Write(byte[] buffer, CancellationToken cancellationToken)
    {
        if(!Writing) return;
        LastMessage = DateTime.Now;
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