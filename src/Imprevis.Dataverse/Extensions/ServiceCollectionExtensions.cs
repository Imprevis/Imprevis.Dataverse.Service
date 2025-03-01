namespace Imprevis.Dataverse.Extensions;

using Imprevis.Dataverse.Abstractions;
using Imprevis.Dataverse.Exceptions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

public static class ServiceCollectionExtensions
{
    public static IDataverseServiceBuilder AddDataverseServices(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        services.AddLogging();

        services.AddOptions<DataverseServiceFactoryOptions>()
            .BindConfiguration(DataverseServiceFactoryOptions.Section)
            .ValidateOnStart();

        services.TryAddSingleton<IDataverseServiceFactory, DataverseServiceFactory>();

        services.TryAddTransient(provider =>
        {
            var factory = provider.GetRequiredService<IDataverseServiceFactory>();
            var resolvers = provider.GetServices<IDataverseServiceResolver>();

            if (factory.GetServices().Count() == 1 && !resolvers.Any())
            {
                return factory.GetServices().First();
            }

            if (factory.GetServices().Count() > 1 && !resolvers.Any())
            {
                throw new DataverseServiceResolverMissingException();
            }

            foreach (var resolver in resolvers)
            {
                var organizationId = resolver.Resolve();
                if (organizationId.HasValue)
                {
                    return factory.GetService(x => x.OrganizationId == organizationId.Value);
                }
            }

            throw new DataverseServiceNotResolvedException();
        });

        return new DataverseServiceBuilder(services);
    }

    public static IDataverseServiceBuilder AddDataverseServices(this IServiceCollection services, Action<DataverseServiceFactoryOptions> configureOptions)
    {
        var builder = services.AddDataverseServices();
        services.Configure(configureOptions);

        return builder;
    }
}
