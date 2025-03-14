namespace Imprevis.Dataverse.Resolvers.Http.UnitTests;

using Imprevis.Dataverse.Abstractions;
using Imprevis.Dataverse.Resolvers.Http;
using Imprevis.Dataverse.Resolvers.Http.UnitTests.Mocks;
using Microsoft.Extensions.DependencyInjection;

public class ResolveByUserClaimExtensionsTests
{
    [Fact]
    public void ResolveByUserClaim_ShouldAddResolver()
    {
        // Arrange
        var collection = new ServiceCollection();

        var builder = new MockDataverseServiceBuilder(collection);
        builder.ResolveByUserClaim("CLAIM_TYPE");

        // Act
        var provider = collection.BuildServiceProvider();
        var resolver = provider.GetService<IDataverseServiceResolver>();

        // Assert
        Assert.NotNull(resolver);
        Assert.IsType<ResolveByUserClaim>(resolver);
    }
}