using Serilog;

namespace Aviator.Main.Handler;

public class ByteHandler : AbstractHandler
{
    public override object? Handle(object data)
    {
        if (data is not byte[] buffer) 
        {
            return null;
        }

        if (buffer.Length < 100)
        {
            return null;
        }
        
        Log.Information("Received {Length}", buffer.Length);

        return base.Handle(buffer);
    }
}