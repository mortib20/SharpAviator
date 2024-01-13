using System.Text.Json;
using System.Text.Json.Nodes;
using Serilog;

namespace Aviator.Main.Handler;

public class JsonHandler : AbstractHandler
{
    public override object? Handle(object data)
    {
        if (data is not byte[] buffer)
        {
            return null;
        }

        try
        {
            var json = JsonNode.Parse(buffer);

            Log.Information("Json {Json}", json?.GetHashCode());
            
            return json;
        }
        catch (JsonException)
        {
            return null;
        }
    }
}