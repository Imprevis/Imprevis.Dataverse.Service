namespace Imprevis.Dataverse.Resolvers.Http.UnitTests;

using Imprevis.Dataverse.Resolvers.Http;
using Microsoft.AspNetCore.Http;

public class ResolveByHeaderTests
{
    [Fact]
    public void Resolve_ShouldReturnNull_WhenHttpContextAccessorIsNull()
    {
        // Arrange
        var resolver = new ResolveByHeader(null, "HEADER_NAME");

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
        var resolver = new ResolveByHeader(contextAccessor, "HEADER_NAME");

        // Act
        var value = resolver.Resolve();

        // Assert
        Assert.Null(value);
    }

    [Fact]
    public void Resolve_ShouldReturnNull_WhenRequestDoesNotContainHeader()
    {
        // Arrange
        var context = new DefaultHttpContext();
        var contextAccessor = new HttpContextAccessor()
        {
            HttpContext = context,
        };

        var resolver = new ResolveByHeader(contextAccessor, "HEADER_NAME");

        // Act
        var value = resolver.Resolve();

        // Assert
        Assert.Null(value);
    }

    [Fact]
    public void Resolve_ShouldReturnNull_WhenHeaderNotParsedSuccessfully()
    {
        // Arrange
        var context = new DefaultHttpContext();
        var contextAccessor = new HttpContextAccessor()
        {
            HttpContext = context,
        };
        context.Request.Headers.Append("HEADER_NAME", "BAD_GUID");

        var resolver = new ResolveByHeader(contextAccessor, "HEADER_NAME");

        // Act
        var actual = resolver.Resolve();

        // Assert
        Assert.Null(actual);
    }

    [Fact]
    public void Resolve_ShouldReturnOrganizationId_WhenSuccessfullyParsesHeader()
    {
        // Arrange
        var organizationId = Guid.NewGuid();

        var context = new DefaultHttpContext();
        var contextAccessor = new HttpContextAccessor()
        {
            HttpContext = context,
        };
        context.Request.Headers.Append("HEADER_NAME", organizationId.ToString());

        var resolver = new ResolveByHeader(contextAccessor, "HEADER_NAME");

        // Act
        var actual = resolver.Resolve();

        // Assert
        Assert.Equal(organizationId, actual);
    }

    [Fact]
    public void Resolve_ShouldReturnOrganizationId_WhenSuccessfullyParsesHeaderWithCustomParser()
    {
        // Arrange
        var organizationId = Guid.NewGuid();

        var context = new DefaultHttpContext();
        var contextAccessor = new HttpContextAccessor()
        {
            HttpContext = context,
        };
        context.Request.Headers.Append("COOKIE_NAME", "--" + organizationId + "--");

        var resolver = new ResolveByHeader(contextAccessor, "COOKIE_NAME", value =>
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