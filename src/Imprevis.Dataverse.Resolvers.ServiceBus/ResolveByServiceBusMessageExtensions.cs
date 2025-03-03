namespace Imprevis.Dataverse.Resolvers.ServiceBus;

using Azure.Messaging.ServiceBus;
using Imprevis.Dataverse.Abstractions;
using Microsoft.Extensions.DependencyInjection;

public static class ResolveByServiceBusMessageExtensions
{
    public static IDataverseServiceBuilder ResolveByServiceBusMessage(this IDataverseServiceBuilder builder)
    {
        builder.Services.AddTransient<IDataverseServiceResolver>(provider =>
        {
            var message = provider.GetService<ServiceBusReceivedMessage>();

            var resolver = new ResolveByServiceBusMessage(message);
            return resolver;
        });

        return builder;
    }

    public static IDataverseServiceBuilder ResolveByServiceBusMessage<TRequest>(this IDataverseServiceBuilder builder, Func<TRequest?, Guid?> parse)
    {
        builder.Services.AddTransient<IDataverseServiceResolver>(provider =>
        {
            var message = provider.GetService<ServiceBusReceivedMessage>();

            var resolver = new ResolveByServiceBusMessage<TRequest>(message, parse);
            return resolver;
        });

        return builder;
    }
}
