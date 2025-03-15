namespace Imprevis.Dataverse.Service.IntegrationTests;

using Imprevis.Dataverse.Service.Abstractions;
using Imprevis.Dataverse.Service.Extensions;
using Imprevis.Dataverse.Service.IntegrationTests.Requests;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public class DataverseServiceTests
{
    [Fact]
    public async Task True()
    {
        var configuration = new ConfigurationBuilder()
            .AddEnvironmentVariables()
            .AddUserSecrets<DataverseServiceTests>()
            .Build();
        
        var collection = new ServiceCollection();
        collection.AddSingleton<IConfiguration>(configuration);
        collection.AddLogging();
        collection.AddDataverseServices();

        var provider = collection.BuildServiceProvider();

        var service = provider.GetRequiredService<IDataverseService>();
        service.Connect();

        var userId = await service.ExecuteAsync(new GetUserIdRequest());

        Assert.NotEqual(Guid.Empty, userId);
    }
}