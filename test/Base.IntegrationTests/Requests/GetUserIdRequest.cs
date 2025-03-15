namespace Imprevis.Dataverse.Service.IntegrationTests.Requests;

using Imprevis.Dataverse.Service.Abstractions;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

class GetUserIdRequest : IDataverseCachedRequest<Guid>
{
    public string CacheKey => "WhoAmI";

    public TimeSpan CacheDuration => TimeSpan.FromMinutes(5);

    public async Task<Guid> Execute(IDataverseService service, ILogger logger, CancellationToken cancellationToken = default)
    {
        Console.WriteLine($"{DateTime.UtcNow}: Executing");
        Thread.Sleep(5000);
        var response = (WhoAmIResponse)await service.ExecuteAsync(new WhoAmIRequest());

        return response.UserId;
    }
}
