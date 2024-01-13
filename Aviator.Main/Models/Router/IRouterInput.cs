namespace Aviator.Main.Models.Router;

public interface IRouterInput
{
    event HandleMessage? HandleMessage;
    Task Start(CancellationToken cancellationToken = default);
    
    void OnHandleMessage(byte[] buffer);
}