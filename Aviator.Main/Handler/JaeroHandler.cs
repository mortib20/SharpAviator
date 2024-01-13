using System.Text.Json;
using System.Text.Json.Nodes;
using Aviator.Main.Models.Acars.Jaero;
using Serilog;

namespace Aviator.Main.Handler;

public class JaeroHandler : AbstractHandler
{
    public override object? Handle(object data)
    {
        if (data is not JsonNode json)
        {
            return null;
        }

        if (json["app"]?["name"]?.ToString() != "JAERO" || json["isu"]?["acars"] is null)
        {
            return null;
        }
        
        Log.Information("Is JAERO");
        
        return JsonSerializer.Deserialize<JaeroFrame>(json.ToJsonString());
    }
}