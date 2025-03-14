namespace Imprevis.Dataverse.UnitTests.Extensions;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

static class ServiceCollectionExtensions
{
    public static void AddConfiguration(this IServiceCollection collection, Dictionary<string, string?>? values = null)
    {
        var configurationBuilder = new ConfigurationBuilder();

        configurationBuilder.AddInMemoryCollection(values);
        var configuration = configurationBuilder.Build();

        collection.AddSingleton<IConfiguration>(configuration);
    }
}
