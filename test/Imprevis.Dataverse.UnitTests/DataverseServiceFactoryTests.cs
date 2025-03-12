namespace Imprevis.Dataverse.UnitTests;

using Imprevis.Dataverse.Abstractions;
using Imprevis.Dataverse.Exceptions;
using Microsoft.Extensions.Caching.Hybrid;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;

public class DataverseServiceFactoryTests
{
    [Fact]
    public void Constructor_ShouldSetServices()
    {
        // Arrange
        var options = GenerateOptions(3);
        var hybridCache = Mock.Of<HybridCache>();
        var cacheFactory = new DataverseServiceCacheFactory(hybridCache);

        // Act
        var factory = new DataverseServiceFactory(Options.Create(options), cacheFactory, new NullLoggerFactory());
        var services = factory.GetServices();

        // Assert
        Assert.Equal(options.Services.Count(), services.Count());
    }

    [Fact]
    public void Dispose_ShouldDisposeAllServices()
    {
        // Arrange
        var service = new Mock<IDataverseService>();

        // Act
        var factory = new DataverseServiceFactory([service.Object]);
        factory.Dispose();

        // Assert
        service.Verify(x => x.Dispose(), Times.Once);
    }

    [Fact]
    public void GetService_ShouldThrowNotFoundException_WhenServiceNotFound()
    {
        // Act
        var factory = new DataverseServiceFactory([]);

        // Assert
        Assert.Throws<DataverseServiceNotFoundException>(() => factory.GetService(x => x.OrganizationId == Guid.NewGuid()));
    }

    [Fact]
    public void GetService_ShouldThrowNotReadyException_WhenServiceNotReady()
    {
        // Arrange
        var organizationId = Guid.NewGuid();
        var service = new Mock<IDataverseService>();
        service.SetupGet(x => x.OrganizationId).Returns(organizationId);
        service.SetupGet(x => x.IsReady).Returns(false);

        // Act
        var factory = new DataverseServiceFactory([service.Object]);

        // Assert
        Assert.Throws<DataverseServiceNotReadyException>(() => factory.GetService(x => x.OrganizationId == organizationId));
    }



    private static DataverseServiceFactoryOptions GenerateOptions(int numberOfService)
    {
        var options = new DataverseServiceFactoryOptions();
        var services = Enumerable.Range(1, numberOfService).Select(x => new DataverseServiceOptions
        {
            OrganizationId = Guid.NewGuid(),
            OrganizationName = $"Organization {x}",
            ConnectionString = string.Empty,
        });

        options.Services = services;

        return options;
    }
}