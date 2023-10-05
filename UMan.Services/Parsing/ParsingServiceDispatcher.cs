using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using UMan.Core.Services;

namespace UMan.Services.Parsing
{
    public class ParsingServiceDispatcher<T> : IParsingServiceDispatcher<T> where T : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;

        public ParsingServiceDispatcher(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            var service = _serviceProvider.GetService<T>() as IHostedService;

            if (service is not null)
            {
                service!.StartAsync(cancellationToken);
            }
            else
            {
                throw new InvalidOperationException("Service not found");
            }

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            var service = _serviceProvider.GetServices<IHostedService>()
                .Where(s => s.GetType() == typeof(T))
                .Single();

            if (service is not null)
            {
                service!.StopAsync(cancellationToken);
            }
            else
            {
                throw new InvalidOperationException("Service not found");
            }

            return Task.CompletedTask;
        }

        public Task Action(Action<T> serviceAction)
        {
            var service = _serviceProvider.GetService<T>() as IHostedService;

            if (service is not null)
            {
                serviceAction.Invoke((T)service);
            }
            else
            {
                throw new InvalidOperationException("Service not found");
            }

            return Task.CompletedTask;
        }
    }
}
