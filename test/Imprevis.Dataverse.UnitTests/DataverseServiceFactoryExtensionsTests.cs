namespace Imprevis.Dataverse.UnitTests;

using Imprevis.Dataverse.Abstractions;
using Imprevis.Dataverse.Exceptions;
using Imprevis.Dataverse.Extensions;
using Moq;

public class DataverseServiceFactoryExtensionsTests
{
    [Fact]
    public void GetServiceByOrganizationId_ShouldReturnService_WhenServiceExists()
    {
        // Arrange
        var organizationId = Guid.NewGuid();
        var service = new Mock<IDataverseService>();
        service.SetupGet(x => x.OrganizationId).Returns(organizationId);
        service.SetupGet(x => x.IsReady).Returns(true);
        var services = new List<IDataverseService>
        {
            service.Object,
        };
        var factory = new DataverseServiceFactory(services);

        // Act
        var result = factory.GetService(organizationId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(organizationId, result.OrganizationId);
        Assert.Throws<DataverseServiceNotFoundException>(() =>
        {
            factory.GetService(Guid.NewGuid());
        });
    }

    [Fact]
    public void GetServiceByName_ShouldReturnService_WhenServiceExists()
    {
        // Arrange
        var organizationName = "Test Org Name";
        var service = new Mock<IDataverseService>();
        service.SetupGet(x => x.OrganizationName).Returns(organizationName);
        service.SetupGet(x => x.IsReady).Returns(true);
        var services = new List<IDataverseService>
        {
            service.Object,
        };
        var factory = new DataverseServiceFactory(services);

        // Act
        var result = factory.GetService(organizationName);
        
        // Assert
        Assert.NotNull(result);
        Assert.Equal(organizationName, result.OrganizationName);
        Assert.Throws<DataverseServiceNotFoundException>(() =>
        {
            factory.GetService("Invalid Org Name");
        });
    }
}