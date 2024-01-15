using Aviator.Main.Models.Delegates;

namespace Aviator.Main.Models.Router.Input;

public interface IRouterInput
{
    event HandleMessage? HandleMessage;
    Task Start(CancellationToken cancellationToken = default);

    void OnHandleMessage(byte[] buffer);
}