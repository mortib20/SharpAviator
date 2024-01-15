namespace Aviator.Main.Models.Router.Output;

public interface IRouterOutput
{
    public bool Connected { get; }
    Task WriteAsync(ReadOnlyMemory<byte> buffer, CancellationToken cancellationToken = default);
}