namespace Imprevis.Dataverse.Resolvers.ServiceBus.UnitTests;

using Imprevis.Dataverse.Abstractions;
using Imprevis.Dataverse.Resolvers.ServiceBus.UnitTests.Mock;
using Imprevis.Dataverse.Resolvers.ServiceBus.UnitTests.Mocks;
using Microsoft.Extensions.DependencyInjection;

public class ResolveByServiceBusMessageExtensionsTests
{
    [Fact]
    public void ResolveByServiceBusMessage_ShouldAddResolver()
    {
        // Arrange
        var collection = new ServiceCollection();

        var builder = new MockDataverseServiceBuilder(collection);
        builder.ResolveByServiceBusMessage();

        // Act
        var provider = collection.BuildServiceProvider();
        var resolver = provider.GetService<IDataverseServiceResolver>();

        // Assert
        Assert.NotNull(resolver);
        Assert.IsType<ResolveByServiceBusMessage>(resolver);
    }

    [Fact]
    public void ResolveByServiceBusMessage_ShouldAddResolverWithParser()
    {
        // Arrange
        var organizationId = Guid.NewGuid();

        var collection = new ServiceCollection();

        var builder = new MockDataverseServiceBuilder(collection);
        builder.ResolveByServiceBusMessage<string>(ParseGuid.Parse);

        // Act
        var provider = collection.BuildServiceProvider();
        var resolver = provider.GetService<IDataverseServiceResolver>();

        // Assert
        Assert.NotNull(resolver);
        Assert.IsType<ResolveByServiceBusMessage<string>>(resolver);
    }
}