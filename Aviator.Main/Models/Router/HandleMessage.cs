namespace Aviator.Main.Models.Router;

public delegate Task HandleMessage(byte[] buffer, CancellationToken cancellationToken = default);