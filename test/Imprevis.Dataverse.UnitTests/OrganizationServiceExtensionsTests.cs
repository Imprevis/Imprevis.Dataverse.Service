namespace Imprevis.Dataverse.UnitTests;

using Imprevis.Dataverse.Abstractions;
using Imprevis.Dataverse.Extensions;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Moq;

public class OrganizationServiceExtensionsTests
{
    [Fact]
    public async Task RetrieveSingle_ShouldReturnNull_WhenNoEntities()
    {
        // Arrange
        var query = new QueryExpression();

        var entities = new EntityCollection();

        var service = new Mock<IDataverseService>();
        service.Setup(x => x.RetrieveMultipleAsync(query, It.IsAny<CancellationToken>())).ReturnsAsync(entities);

        // Act
        var result = await service.Object.RetrieveSingleAsync(query);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task RetrieveSingle_ShouldReturnEntity_WhenOnlyOneEntity()
    {
        // Arrange
        var query = new QueryExpression();

        var entity = new Entity();
        var entities = new EntityCollection { Entities = { entity } };

        var service = new Mock<IDataverseService>();
        service.Setup(x => x.RetrieveMultipleAsync(query, It.IsAny<CancellationToken>())).ReturnsAsync(entities);

        // Act
        var result = await service.Object.RetrieveSingleAsync(query);

        // Assert
        Assert.Equal(entity, result);
    }

    [Fact]
    public async Task RetrieveSingle_ShouldThrowException_WhenMultipleEntities()
    {
        // Arrange
        var query = new QueryExpression();

        var entities = new EntityCollection
        {
            Entities =
            {
                new Entity(),
                new Entity(),
            }
        };

        var service = new Mock<IDataverseService>();
        service.Setup(x => x.RetrieveMultipleAsync(query, It.IsAny<CancellationToken>())).ReturnsAsync(entities);

        // Act + Assert
        await Assert.ThrowsAsync<Exception>(() => service.Object.RetrieveSingleAsync(query));
    }
}