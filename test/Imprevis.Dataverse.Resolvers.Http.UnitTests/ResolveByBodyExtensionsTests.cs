namespace Imprevis.Dataverse.Resolvers.Http.UnitTests;

using Imprevis.Dataverse.Abstractions;
using Imprevis.Dataverse.Resolvers.Http;
using Imprevis.Dataverse.Resolvers.Http.UnitTests.Mocks;
using Imprevis.Dataverse.Resolvers.Http.UnitTests.Parsers;
using Microsoft.Extensions.DependencyInjection;

public class ResolveByBodyExtensionsTests
{
    [Fact]
    public void ResolveByBody_ShouldAddResolver()
    {
        // Arrange
        var collection = new ServiceCollection();

        var builder = new MockDataverseServiceBuilder(collection);
        builder.ResolveByBody();

        // Act
        var provider = collection.BuildServiceProvider();
        var resolver = provider.GetService<IDataverseServiceResolver>();

        // Assert
        Assert.NotNull(resolver);
        Assert.IsType<ResolveByBody>(resolver);
    }

    [Fact]
    public void ResolveByBody_ShouldAddResolverWithParser()
    {
        // Arrange
        var collection = new ServiceCollection();

        var builder = new MockDataverseServiceBuilder(collection);
        builder.ResolveByBody<string>(ParseGuid.Parse);

        // Act
        var provider = collection.BuildServiceProvider();
        var resolver = provider.GetService<IDataverseServiceResolver>();

        // Assert
        Assert.NotNull(resolver);
        Assert.IsType<ResolveByBody<string>>(resolver);
    }
}