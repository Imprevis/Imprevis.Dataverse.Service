namespace Imprevis.Dataverse.Resolvers.Http.UnitTests;

using Imprevis.Dataverse.Abstractions;
using Imprevis.Dataverse.Resolvers.Http;
using Imprevis.Dataverse.Resolvers.Http.UnitTests.Mocks;
using Microsoft.Extensions.DependencyInjection;

public class ResolveByQueryStringExtensionsTests
{
    [Fact]
    public void ResolveByQ_ShouldAddResolver()
    {
        // Arrange
        var collection = new ServiceCollection();

        var builder = new MockDataverseServiceBuilder(collection);
        builder.ResolveByQueryString("PARAMETER_NAME");

        // Act
        var provider = collection.BuildServiceProvider();
        var resolver = provider.GetService<IDataverseServiceResolver>();

        // Assert
        Assert.NotNull(resolver);
        Assert.IsType<ResolveByQueryString>(resolver);
    }
}