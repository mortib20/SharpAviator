using System.Text.Json;
using System.Text.Json.Nodes;
using Aviator.Main.Models.Acars.DumpVDL2;
using Serilog;

namespace Aviator.Main.Handler;

public class DumpVdl2Handler : AbstractHandler
{
    public override object? Handle(object data)
    {
        if (data is not JsonNode json)
        {
            return null;
        }

        if (json["vdl2"] is null || json["vdl2"]?["app"]?["name"]?.ToString() != "dumpvdl2")
        {
            return null;
        }
        
        Log.Information("Is DumpVDL2");
        
        return JsonSerializer.Deserialize<DumpVdl2Frame>(json.ToJsonString());
    }
}