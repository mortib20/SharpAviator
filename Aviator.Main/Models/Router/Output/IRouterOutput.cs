namespace Aviator.Main.Models.Router.Output;

public interface IRouterOutput
{
    ValueTask Write(ReadOnlyMemory<byte> buffer, CancellationToken cancellationToken = default);
}