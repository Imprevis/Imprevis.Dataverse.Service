namespace Imprevis.Dataverse.Resolvers.ServiceBus;

using Azure.Messaging.ServiceBus;
using Imprevis.Dataverse.Abstractions;
using System.Text.Json;

public class ResolveByServiceBusMessage<TRequest>(ServiceBusReceivedMessage? message, Func<TRequest?, Guid?> parse) : IDataverseServiceResolver
{
    public Guid? Resolve()
    {
        if (message == null)
        {
            return null;
        }

        try
        {
            var body = message.Body.ToObjectFromJson<TRequest>();
            if (body == null)
            {
                return null;
            }

            return parse(body);
        }
        catch (JsonException)
        {
            return null;
        }
    }
}

public class ResolveByServiceBusMessage(ServiceBusReceivedMessage? message) : IDataverseServiceResolver
{
    public Guid? Resolve()
    {
        static Guid? parser(string? value)
        {
            var parsed = Guid.TryParse(value, out var organizationId);
            if (parsed)
            {
                return organizationId;
            }

            return null;
        }

        return new ResolveByServiceBusMessage<string>(message, parser).Resolve();
    }
}