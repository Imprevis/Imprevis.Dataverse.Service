namespace Imprevis.Dataverse.Resolvers.Http.UnitTests;

using Imprevis.Dataverse.Resolvers.Http;
using Imprevis.Dataverse.Resolvers.Http.UnitTests.Mocks;
using Microsoft.AspNetCore.Http;

public class ResolveByCookieTests
{
    [Fact]
    public void Resolve_ShouldReturnNull_WhenHttpContextAccessorIsNull()
    {
        // Arrange
        var resolver = new ResolveByCookie(null, "COOKIE_NAME");

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
        var resolver = new ResolveByCookie(contextAccessor, "COOKIE_NAME");

        // Act
        var value = resolver.Resolve();

        // Assert
        Assert.Null(value);
    }

    [Fact]
    public void Resolve_ShouldReturnNull_WhenRequestDoesNotContainCookie()
    {
        // Arrange
        var context = new DefaultHttpContext();
        var contextAccessor = new HttpContextAccessor()
        {
            HttpContext = context,
        };

        var resolver = new ResolveByCookie(contextAccessor, "COOKIE_NAME");

        // Act
        var value = resolver.Resolve();

        // Assert
        Assert.Null(value);
    }

    [Fact]
    public void Resolve_ShouldReturnNull_WhenCookieNotParsedSuccessfully()
    {
        // Arrange
        var context = new DefaultHttpContext();
        var contextAccessor = new HttpContextAccessor()
        {
            HttpContext = context,
        };
        var cookies = new MockCookieCollection()
        {
            { "COOKIE_NAME",  "BAD_GUID" }
        };
        context.Request.Cookies = cookies;

        var resolver = new ResolveByCookie(contextAccessor, "COOKIE_NAME");

        // Act
        var actual = resolver.Resolve();

        // Assert
        Assert.Null(actual);
    }

    [Fact]
    public void Resolve_ShouldReturnOrganizationId_WhenSuccessfullyParsesCookie()
    {
        // Arrange
        var organizationId = Guid.NewGuid();

        var context = new DefaultHttpContext();
        var contextAccessor = new HttpContextAccessor()
        {
            HttpContext = context,
        };
        var cookies = new MockCookieCollection
        {
            { "COOKIE_NAME",  organizationId.ToString() }
        };
        context.Request.Cookies = cookies;

        var resolver = new ResolveByCookie(contextAccessor, "COOKIE_NAME");

        // Act
        var actual = resolver.Resolve();

        // Assert
        Assert.Equal(organizationId, actual);
    }

    [Fact]
    public void Resolve_ShouldReturnOrganizationId_WhenSuccessfullyParsesCookieWithCustomParser()
    {
        // Arrange
        var organizationId = Guid.NewGuid();

        var context = new DefaultHttpContext();
        var contextAccessor = new HttpContextAccessor()
        {
            HttpContext = context,
        };
        var cookies = new MockCookieCollection
        {
            { "COOKIE_NAME",  "--" + organizationId + "--" }
        };
        context.Request.Cookies = cookies;

        var resolver = new ResolveByCookie(contextAccessor, "COOKIE_NAME", value =>
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