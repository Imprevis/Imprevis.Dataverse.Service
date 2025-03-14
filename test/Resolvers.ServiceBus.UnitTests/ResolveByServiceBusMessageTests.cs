namespace Imprevis.Dataverse.Resolvers.ServiceBus.UnitTests;

using Azure.Messaging.ServiceBus;
using System.Text.Json;

public class ResolveByServiceBusMessageTests
{
    [Fact]
    public void Resolve_ShouldReturnNull_WhenMessageIsNull()
    {
        // Arrange
        var resolver = new ResolveByServiceBusMessage(null);

        // Act
        var value = resolver.Resolve();

        // Assert
        Assert.Null(value);
    }

    [Fact]
    public void Resolve_ShouldReturnNull_WhenMessageBodyIsNull()
    {
        // Arrange
        var message = ServiceBusModelFactory.ServiceBusReceivedMessage(null);
        var resolver = new ResolveByServiceBusMessage(message);

        // Act
        var value = resolver.Resolve();

        // Assert
        Assert.Null(value);
    }

    [Fact]
    public void Resolve_ShouldReturnNull_WhenMessageBodyDeserializesToNull()
    {
        // Arrange
        var message = ServiceBusModelFactory.ServiceBusReceivedMessage(new BinaryData("null"));
        var resolver = new ResolveByServiceBusMessage(message);

        // Act
        var value = resolver.Resolve();

        // Assert
        Assert.Null(value);
    }

    [Fact]
    public void Resolve_ShouldReturnOrganizationId_WhenMessageBodyIsGuid()
    {
        // Arrange
        var organizationId = Guid.NewGuid();

        var message = ServiceBusModelFactory.ServiceBusReceivedMessage(new BinaryData($"\"{organizationId}\""));
        var resolver = new ResolveByServiceBusMessage(message);

        // Act
        var value = resolver.Resolve();

        // Assert
        Assert.Equal(organizationId, value);
    }

    [Fact]
    public void Resolve_ShouldReturnNull_WhenMessageBodyIsNotGuid()
    {
        // Arrange
        var message = ServiceBusModelFactory.ServiceBusReceivedMessage(new BinaryData($"\"BAD_GUID\""));
        var resolver = new ResolveByServiceBusMessage(message);

        // Act
        var value = resolver.Resolve();

        // Assert
        Assert.Null(value);
    }
}