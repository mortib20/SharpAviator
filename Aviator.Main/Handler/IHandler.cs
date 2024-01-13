namespace Aviator.Main.Handler;

public interface IHandler
{
    public IHandler NextHandler(IHandler handler);

    public object? Handle(object buffer);
}