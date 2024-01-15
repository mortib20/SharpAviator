namespace Aviator.Main.Models.Delegates;

public delegate Task HandleMessage(byte[] buffer, CancellationToken cancellationToken = default);