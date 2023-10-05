namespace UMan.Core.Services
{
    public interface IParsingServiceDispatcher<T>
    {
        Task StopAsync(CancellationToken cancellationToken);

        Task StartAsync(CancellationToken cancellationToken);

        Task Action(Action<T> serviceAction);
    }
}
