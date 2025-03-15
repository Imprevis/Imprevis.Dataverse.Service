namespace Imprevis.Dataverse.Service.UnitTests.Extensions;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static void AddConfiguration(this IServiceCollection collection, Dictionary<string, string?>? values = null)
    {
        var configurationBuilder = new ConfigurationBuilder();

        configurationBuilder.AddInMemoryCollection(values);
        var configuration = configurationBuilder.Build();

        collection.AddSingleton<IConfiguration>(configuration);
    }
}
