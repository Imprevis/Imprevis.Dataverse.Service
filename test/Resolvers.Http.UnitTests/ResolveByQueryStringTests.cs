namespace Imprevis.Dataverse.Service.Resolvers.Http.UnitTests;

using Imprevis.Dataverse.Service.Resolvers.Http;
using Microsoft.AspNetCore.Http;

public class ResolveByQueryStringTests
{
    [Fact]
    public void Resolve_ShouldReturnNull_WhenHttpContextAccessorIsNull()
    {
        // Arrange
        var resolver = new ResolveByQueryString(null, "PARAMETER_NAME");

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
        var resolver = new ResolveByQueryString(contextAccessor, "PARAMETER_NAME");

        // Act
        var value = resolver.Resolve();

        // Assert
        Assert.Null(value);
    }

    [Fact]
    public void Resolve_ShouldReturnNull_WhenRequestDoesNotContainQueryParameter()
    {
        // Arrange
        var context = new DefaultHttpContext();
        var contextAccessor = new HttpContextAccessor()
        {
            HttpContext = context,
        };

        var resolver = new ResolveByQueryString(contextAccessor, "PARAMETER_NAME");

        // Act
        var value = resolver.Resolve();

        // Assert
        Assert.Null(value);
    }

    [Fact]
    public void Resolve_ShouldReturnNull_WhenQueryParameterNotParsedSuccessfully()
    {
        // Arrange
        var context = new DefaultHttpContext();
        var contextAccessor = new HttpContextAccessor()
        {
            HttpContext = context,
        };
        context.Request.QueryString = new QueryString("?PARAMETER_NAME=BAD_GUID");

        var resolver = new ResolveByQueryString(contextAccessor, "PARAMETER_NAME");

        // Act
        var actual = resolver.Resolve();

        // Assert
        Assert.Null(actual);
    }

    [Fact]
    public void Resolve_ShouldReturnOrganizationId_WhenSuccessfullyParsesQueryParameter()
    {
        // Arrange
        var organizationId = Guid.NewGuid();

        var context = new DefaultHttpContext();
        var contextAccessor = new HttpContextAccessor()
        {
            HttpContext = context,
        };
        context.Request.QueryString = new QueryString($"?PARAMETER_NAME={organizationId}");

        var resolver = new ResolveByQueryString(contextAccessor, "PARAMETER_NAME");

        // Act
        var actual = resolver.Resolve();

        // Assert
        Assert.Equal(organizationId, actual);
    }

    [Fact]
    public void Resolve_ShouldReturnOrganizationId_WhenSuccessfullyParsesQueryParameterWithCustomParser()
    {
        // Arrange
        var organizationId = Guid.NewGuid();

        var context = new DefaultHttpContext();
        var contextAccessor = new HttpContextAccessor()
        {
            HttpContext = context,
        };
        context.Request.QueryString = new QueryString($"?PARAMETER_NAME=--{organizationId}--");

        var resolver = new ResolveByQueryString(contextAccessor, "PARAMETER_NAME", value =>
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