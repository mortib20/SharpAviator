namespace Aviator.Main.Models.Router.Output;

public interface IRouterOutput
{
    Task Write(ReadOnlyMemory<byte> buffer, CancellationToken cancellationToken = default);
}