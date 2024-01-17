using System.Collections.Immutable;
using Aviator.Main.Database;
using Aviator.Main.Models;
using Aviator.Main.Models.Router.Output;
using Endpoint = Aviator.Main.Models.Endpoint;

namespace Aviator.Main.Services;

public class AviatorRouterOutputService(AviatorRepository aviatorRepository, ILoggerFactory loggerFactory)
{
    public Dictionary<Decoder, List<AviatorRouterOutput>> Outputs =
        CreateOutputs(aviatorRepository.AviatorContext.Endpoints.ToImmutableList(), loggerFactory);

    public void Restart()
    {
        Outputs = CreateOutputs(aviatorRepository.AviatorContext.Endpoints.ToImmutableList(), loggerFactory);
    }

    private static Dictionary<Decoder, List<AviatorRouterOutput>> CreateOutputs(IList<Endpoint> endpoints,
        ILoggerFactory loggerFactory)
    {
        var keys = endpoints.Select(s => s.Decoder).Distinct().ToHashSet();

        return keys
            .ToDictionary(decoder => decoder,
                decoder => endpoints.Where(d => d.Decoder.Equals(decoder))
                    .Select(endpoint => new AviatorRouterOutput(endpoint, loggerFactory)).ToList());
    }
}