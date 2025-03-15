namespace Imprevis.Dataverse.Service;

using Imprevis.Dataverse.Service.Abstractions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

internal class DataverseServiceWarmupService(IDataverseServiceFactory factory, ILogger<DataverseServiceWarmupService> logger) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation("Warming up Dataverse services.");

        while (true)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                logger.LogInformation("Cancelling the Dataverse warmup service.");
                return;
            }

            var ready = ConnectAll();
            if (ready)
            {
                logger.LogInformation("All Dataverse services have been warmed up.");
                return;
            }

            // Wait and try again.
            await Task.Delay(TimeSpan.FromMinutes(1), cancellationToken);
        }
    }

    private bool ConnectAll()
    {
        var services = factory.GetServices(x => !x.IsReady);

        Parallel.ForEach(services, service => service.Connect());

        return services.All(x => x.IsReady);
    }
}