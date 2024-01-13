using System.Text.Json;
using System.Text.Json.Nodes;
using Aviator.Main.Models.Acars.Acarsdec;
using Aviator.Main.Models.Acars.Jaero;
using Serilog;

namespace Aviator.Main.Handler;

public class AcarsdecHandler : AbstractHandler
{
    public override object? Handle(object data)
    {
        if (data is not JsonNode json)
        {
            return null;
        }

        if (json["app"]?["name"]?.ToString() != "acarsdec")
        {
            return null;
        }
        
        Log.Information("Is Acarsdec");
        
        return JsonSerializer.Deserialize<AcarsdecFrame>(json.ToJsonString());
    }
}