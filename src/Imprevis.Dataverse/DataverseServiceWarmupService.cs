namespace Imprevis.Dataverse;

using Imprevis.Dataverse.Abstractions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

internal class DataverseServiceWarmupService(IDataverseServiceFactory factory, ILogger<DataverseServiceWarmupService> logger) : IHostedService, IAsyncDisposable
{
    private Timer? timer = null;

    public Task StartAsync(CancellationToken cancellationToken = default)
    {
        logger.LogInformation("CdsServiceWarmupService is starting.");

        // Start timer to connect all services. Use a timer so only one thread tries
        timer = new Timer(DoWork, null, TimeSpan.FromMinutes(0), TimeSpan.FromMinutes(1));

        return Task.CompletedTask;
    }

    private async void DoWork(object? state)
    {
        var services = factory.GetServices(x => !x.IsReady);

        Parallel.ForEach(services, service => service.Connect());

        if (services.All(x => x.IsReady))
        {
            await StopAsync();
            await DisposeAsync();
        }
    }

    public Task StopAsync(CancellationToken cancellationToken = default)
    {
        logger.LogInformation("CdsServiceWarmupService is stopping.");

        timer?.Change(Timeout.Infinite, 0);

        return Task.CompletedTask;
    }

    public async ValueTask DisposeAsync()
    {
        if (timer != null)
        {
            await timer.DisposeAsync();
        }
    }
}
