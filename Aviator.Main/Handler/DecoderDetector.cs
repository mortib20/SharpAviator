using System.Text.Json.Nodes;
using Aviator.Main.Models;

namespace Aviator.Main.Handler;

public abstract class DecoderDetector
{
    public static Decoder? Detect(JsonNode data)
    {
        if (data["app"]?["name"]?.ToString() == "JAERO" && data["isu"]?["acars"] is not null)
        {
            return Decoder.Jaero;
        }

        if (data["vdl2"] is not null && data["vdl2"]?["app"]?["name"]?.ToString() == "dumpvdl2")
        {
            return Decoder.DumpVdl2;
        }
        
        if (data["app"]?["name"]?.ToString() != "acarsdec")
        {
            return Decoder.Acarsdec;
        }

        return null;
    }
}