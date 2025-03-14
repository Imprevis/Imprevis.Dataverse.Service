namespace Imprevis.Dataverse.UnitTests;

using Microsoft.Extensions.Caching.Hybrid;

public class DataverseServiceCacheTests
{
    [Fact]
    public async Task True()
    {
        // Arrange
        var cacheKey = "key";
        var cacheDuration = TimeSpan.FromMinutes(Random.Shared.Next());
        var organizationId = Guid.NewGuid();

        var hybridCache = new Mock<HybridCache>();
        var cacheService = new DataverseServiceCache(hybridCache.Object, organizationId);

        // Act
        await cacheService.GetOrCreate(cacheKey, (cancellation) => ValueTask.FromResult("value"), cacheDuration);

        // Assert
        hybridCache.Verify(x => x.GetOrCreateAsync(
                It.Is<string>(x => x.Contains(organizationId.ToString()) && x.Contains(cacheKey)),
                It.IsAny<It.IsAnyType>(),
                It.IsAny<Func<It.IsAnyType, CancellationToken, ValueTask<It.IsAnyType>>>(),
                It.Is<HybridCacheEntryOptions>(x => x.Expiration == cacheDuration),
                null,
                It.IsAny<CancellationToken>()
            ), Times.Once());

    }
}