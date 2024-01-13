namespace Aviator.Main.Handler;

public abstract class AbstractHandler : IHandler
{
    private IHandler? _nextHandler;
    
    public IHandler NextHandler(IHandler handler)
    {
        _nextHandler = handler;

        return handler;
    }

    public virtual object? Handle(object data)
    {
        return _nextHandler is null ? data : _nextHandler?.Handle(data);
    }
}