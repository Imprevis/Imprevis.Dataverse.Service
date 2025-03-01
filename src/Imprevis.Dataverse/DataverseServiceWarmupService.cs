namespace Imprevis.Dataverse;

using Imprevis.Dataverse.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

internal class DataverseServiceWarmupService(IServiceProvider serviceProvider) : IHostedService
{
    public Task StartAsync(CancellationToken cancellationToken = default)
    {
        using var scope = serviceProvider.CreateScope();

        // Get factory to initialize connections
        var factory = scope.ServiceProvider.GetRequiredService<IDataverseServiceFactory>();

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken = default) => Task.CompletedTask;
}
