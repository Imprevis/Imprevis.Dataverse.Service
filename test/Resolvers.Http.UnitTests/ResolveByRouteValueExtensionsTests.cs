namespace Imprevis.Dataverse.Service.Resolvers.Http.UnitTests;

using Imprevis.Dataverse.Service.Abstractions;
using Imprevis.Dataverse.Service.Resolvers.Http;
using Imprevis.Dataverse.Service.Resolvers.Http.UnitTests.Mocks;
using Microsoft.Extensions.DependencyInjection;

public class ResolveByRouteValueExtensionsTests
{
    [Fact]
    public void ResolveByRouteValue_ShouldAddResolver()
    {
        // Arrange
        var collection = new ServiceCollection();

        var builder = new MockDataverseServiceBuilder(collection);
        builder.ResolveByRouteValue("ROUTE_NAME");

        // Act
        var provider = collection.BuildServiceProvider();
        var resolver = provider.GetService<IDataverseServiceResolver>();

        // Assert
        Assert.NotNull(resolver);
        Assert.IsType<ResolveByRouteValue>(resolver);
    }
}