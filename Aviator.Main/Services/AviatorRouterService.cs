using System.Runtime.CompilerServices;
using Aviator.Main.Database;
using Aviator.Main.Models;
using Aviator.Main.Models.Router.Input;
using Aviator.Main.Models.Router.Output;
using Endpoint = Aviator.Main.Models.Endpoint;

namespace Aviator.Main.Services;

public class AviatorRouterService(IServiceScopeFactory scopeFactory, ILoggerFactory loggerFactory)
{
    public IRouterInput RouterInput { get; } = new UdpRouterInput(21000, loggerFactory);
    public Dictionary<Decoder, List<IRouterOutput?>> Outputs = EndpointsToOutput(GetContext(scopeFactory), loggerFactory);

    private static Dictionary<Decoder, List<IRouterOutput?>> EndpointsToOutput(AviatorContext aviatorContext, ILoggerFactory loggerFactory)
    {
        var endpoints = EndpointsFromDatabase(aviatorContext);

        return endpoints
            .ToDictionary(s => s.Decoder, v =>
                endpoints.Where(b => b.Decoder == v.Decoder).Select<Endpoint, IRouterOutput?>(s =>
                {
                    return s.Protocol switch
                    {
                        Protocol.Udp => new UdpRouterOutput(s.Address, s.Port, loggerFactory),
                        Protocol.Tcp => new TcpRouterOutput(s.Address, s.Port, loggerFactory),
                        _ => throw new SwitchExpressionException("Unsupported protocol")
                    };
                }).Where(s => s is not null).ToList());
    }

    private static List<Endpoint> EndpointsFromDatabase(AviatorContext aviatorContext)
    {
        return aviatorContext.Outputs.ToList();
    }

    private static AviatorContext GetContext(IServiceScopeFactory scopeFactory)
    {
        return scopeFactory.CreateScope().ServiceProvider.GetRequiredService<AviatorContext>();
    }
}