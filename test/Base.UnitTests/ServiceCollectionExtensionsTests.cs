namespace Imprevis.Dataverse.Service.UnitTests;

using Imprevis.Dataverse.Service.Abstractions;
using Imprevis.Dataverse.Service.Exceptions;
using Imprevis.Dataverse.Service.Extensions;
using Imprevis.Dataverse.Service.UnitTests.Extensions;
using Microsoft.Extensions.Caching.Hybrid;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

public class ServiceCollectionExtensionsTests
{
    [Fact]
    public void AddDataverseServices_ShouldRegisterServices()
    {
        // Arrange
        var collection = new ServiceCollection();
        collection.AddConfiguration();
        collection.AddDataverseServices();

        // Act
        var provider = collection.BuildServiceProvider();

        // Assert
        var loggerFactory = provider.GetService<ILoggerFactory>();
        Assert.NotNull(loggerFactory);

        var cacheService = provider.GetService<HybridCache>();
        Assert.NotNull(cacheService);

        var cacheServiceFactory = provider.GetService<IDataverseServiceCacheFactory>();
        Assert.NotNull(cacheServiceFactory);

        var serviceFactory = provider.GetService<IDataverseServiceFactory>();
        Assert.NotNull(serviceFactory);
    }

    [Fact]
    public void GetService_ShouldThrowException_WhenNoServiceRegistered()
    {
        // Arrange
        var collection = new ServiceCollection();
        collection.AddConfiguration();
        collection.AddDataverseServices();

        // Act
        var provider = collection.BuildServiceProvider();

        // Assert
        Assert.Throws<DataverseServiceNotResolvedException>(() => provider.GetService<IDataverseService>());
    }

    [Fact]
    public void GetService_ShouldReturnService_WhenOneServiceAndNoResolvers()
    {
        // Arrange
        var collection = new ServiceCollection();
        collection.AddConfiguration();
        collection.AddDataverseServices(options =>
        {
            options.Services =
            [
                new DataverseServiceOptions
                {
                     OrganizationId = Guid.NewGuid(),
                     OrganizationName = "Test Org 1",
                     ConnectionString = string.Empty,
                }
            ];
        });

        // Act
        var provider = collection.BuildServiceProvider();
        var service = provider.GetService<IDataverseService>();

        // Assert
        Assert.NotNull(service);
    }

    [Fact]
    public void GetService_ShouldThrowException_WhenMultipleServicesAndNoResolvers()
    {
        // Arrange
        var collection = new ServiceCollection();
        collection.AddConfiguration();
        collection.AddDataverseServices(options =>
        {
            options.Services =
            [
                new DataverseServiceOptions
                {
                     OrganizationId = Guid.NewGuid(),
                     OrganizationName = "Test Org 1",
                     ConnectionString = string.Empty,
                },
                new DataverseServiceOptions
                {
                     OrganizationId = Guid.NewGuid(),
                     OrganizationName = "Test Org 2",
                     ConnectionString = string.Empty,
                },
            ];
        });

        // Act
        var provider = collection.BuildServiceProvider();

        // Assert
        Assert.Throws<DataverseServiceResolverMissingException>(() => provider.GetService<IDataverseService>());
    }

    [Fact]
    public void AddDataverseServices_ShouldAddOptions()
    {
        // Arrange
        var collection = new ServiceCollection();
        collection.AddConfiguration();
        collection.AddDataverseServices(options =>
        {
            options.Services =
            [
                new DataverseServiceOptions
                {
                     OrganizationId = Guid.NewGuid(),
                     OrganizationName = "Test Org 1",
                     ConnectionString = string.Empty,
                }
            ];
        });

        // Act
        var provider = collection.BuildServiceProvider();

        // Assert
        var options = provider.GetService<IOptions<DataverseServiceFactoryOptions>>();

        Assert.NotNull(options);
        Assert.Single(options.Value.Services);
    }
}