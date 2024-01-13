using System.Text.Json;
using System.Text.Json.Nodes;
using Serilog;

namespace Aviator.Main.Handler;

public abstract class JsonHelper
{
    public static JsonNode? Parse(ReadOnlySpan<byte> buffer)
    {
        try
        {
            return JsonNode.Parse(buffer);
        }
        catch (JsonException)
        {
            return null;
        }
    }
}