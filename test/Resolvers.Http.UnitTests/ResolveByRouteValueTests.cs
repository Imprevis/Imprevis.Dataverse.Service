namespace Imprevis.Dataverse.Service.Resolvers.Http.UnitTests;

using Imprevis.Dataverse.Service.Resolvers.Http;
using Microsoft.AspNetCore.Http;

public class ResolveByRouteValueTests
{
    [Fact]
    public void Resolve_ShouldReturnNull_WhenHttpContextAccessorIsNull()
    {
        // Arrange
        var resolver = new ResolveByRouteValue(null, "ROUTE_NAME");

        // Act
        var value = resolver.Resolve();

        // Assert
        Assert.Null(value);
    }

    [Fact]
    public void Resolve_ShouldReturnNull_WhenHttpContextIsNull()
    {
        // Arrange
        var contextAccessor = new HttpContextAccessor();
        var resolver = new ResolveByRouteValue(contextAccessor, "ROUTE_NAME");

        // Act
        var value = resolver.Resolve();

        // Assert
        Assert.Null(value);
    }

    [Fact]
    public void Resolve_ShouldReturnNull_WhenRequestDoesNotContainRouteValue()
    {
        // Arrange
        var context = new DefaultHttpContext();
        var contextAccessor = new HttpContextAccessor()
        {
            HttpContext = context,
        };

        var resolver = new ResolveByRouteValue(contextAccessor, "ROUTE_NAME");

        // Act
        var value = resolver.Resolve();

        // Assert
        Assert.Null(value);
    }

    [Fact]
    public void Resolve_ShouldReturnNull_WhenRouteValueNotParsedSuccessfully()
    {
        // Arrange
        var context = new DefaultHttpContext();
        var contextAccessor = new HttpContextAccessor()
        {
            HttpContext = context,
        };
        context.Request.RouteValues.Add("ROUTE_NAME", "BAD_GUID");

        var resolver = new ResolveByRouteValue(contextAccessor, "ROUTE_NAME");

        // Act
        var actual = resolver.Resolve();

        // Assert
        Assert.Null(actual);
    }

    [Fact]
    public void Resolve_ShouldReturnOrganizationId_WhenSuccessfullyParsesRouteValue()
    {
        // Arrange
        var organizationId = Guid.NewGuid();

        var context = new DefaultHttpContext();
        var contextAccessor = new HttpContextAccessor()
        {
            HttpContext = context,
        };
        context.Request.RouteValues.Add("ROUTE_NAME", organizationId.ToString());

        var resolver = new ResolveByRouteValue(contextAccessor, "ROUTE_NAME");

        // Act
        var actual = resolver.Resolve();

        // Assert
        Assert.Equal(organizationId, actual);
    }


    [Fact]
    public void Resolve_ShouldReturnNull_WhenSuccessfullyParsesNullRouteValue()
    {
        // Arrange
        var context = new DefaultHttpContext();
        var contextAccessor = new HttpContextAccessor()
        {
            HttpContext = context,
        };
        context.Request.RouteValues.Add("ROUTE_NAME", null);

        var resolver = new ResolveByRouteValue(contextAccessor, "ROUTE_NAME");

        // Act
        var actual = resolver.Resolve();

        // Assert
        Assert.Null(actual);
    }

    [Fact]
    public void Resolve_ShouldReturnOrganizationId_WhenSuccessfullyParsesRouteValueWithCustomParser()
    {
        // Arrange
        var organizationId = Guid.NewGuid();

        var context = new DefaultHttpContext();
        var contextAccessor = new HttpContextAccessor()
        {
            HttpContext = context,
        };
        context.Request.RouteValues.Add("ROUTE_NAME", "--" + organizationId + "--");

        var resolver = new ResolveByRouteValue(contextAccessor, "ROUTE_NAME", value =>
        {
            _ = Guid.TryParse(value?.Replace("--", string.Empty), out var result);
            return result;
        });

        // Act
        var actual = resolver.Resolve();

        // Assert
        Assert.Equal(organizationId, actual);
    }
}