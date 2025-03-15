namespace Imprevis.Dataverse.Service.Resolvers.Http.UnitTests;

using Imprevis.Dataverse.Service.Abstractions;
using Imprevis.Dataverse.Service.Resolvers.Http;
using Imprevis.Dataverse.Service.Resolvers.Http.UnitTests.Mocks;
using Microsoft.Extensions.DependencyInjection;

public class ResolveByCookieExtensionsTests
{
    [Fact]
    public void ResolveByCookie_ShouldAddResolver()
    {
        // Arrange
        var collection = new ServiceCollection();

        var builder = new MockDataverseServiceBuilder(collection);
        builder.ResolveByCookie("COOKIE_NAME");

        // Act
        var provider = collection.BuildServiceProvider();
        var resolver = provider.GetService<IDataverseServiceResolver>();

        // Assert
        Assert.NotNull(resolver);
        Assert.IsType<ResolveByCookie>(resolver);
    }
}