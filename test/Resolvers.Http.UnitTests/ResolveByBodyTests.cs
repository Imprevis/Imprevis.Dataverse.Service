namespace Imprevis.Dataverse.Service.Resolvers.Http.UnitTests;

using Imprevis.Dataverse.Service.Resolvers.Http;
using Imprevis.Dataverse.Service.Resolvers.Http.UnitTests.Mocks;
using Imprevis.Dataverse.Service.Resolvers.Http.UnitTests.Parsers;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

public class ResolveByBodyTests
{
    [Fact]
    public void Resolve_ShouldReturnNull_WhenHttpContextAccessorIsNull()
    {
        // Arrange
        var resolver = new ResolveByBody<string>(null, ParseGuid.Parse);

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
        var resolver = new ResolveByBody<string>(contextAccessor, ParseGuid.Parse);

        // Act
        var value = resolver.Resolve();

        // Assert
        Assert.Null(value);
    }

    [Fact]
    public void Resolve_ShouldReturnNull_WhenUnsuccessfullyParsesBody()
    {
        // Arrange
        var organizationId = Guid.NewGuid();

        var context = new DefaultHttpContext();
        var contextAccessor = new HttpContextAccessor()
        {
            HttpContext = context,
        };

        using var stream = new MemoryStream();
        JsonSerializer.Serialize(stream, 12345); // Write something that's not a string
        context.Request.Body = stream;
        context.Request.ContentLength = stream.Length;

        var resolver = new ResolveByBody<string>(contextAccessor, ParseGuid.Parse);

        // Act
        var value = resolver.Resolve();

        // Assert
        Assert.Null(value);
    }

    [Fact]
    public void Resolve_ShouldReturnOrganizationId_WhenSuccessfullyParsesBody()
    {
        // Arrange
        var organizationId = Guid.NewGuid();

        var context = new DefaultHttpContext();
        var contextAccessor = new HttpContextAccessor()
        {
            HttpContext = context,
        };

        using var stream = new MemoryStream();
        JsonSerializer.Serialize(stream, organizationId.ToString());
        context.Request.Body = stream;
        context.Request.ContentLength = stream.Length;

        var resolver = new ResolveByBody<string>(contextAccessor, ParseGuid.Parse);

        // Act
        var actual = resolver.Resolve();

        // Assert
        Assert.Equal(organizationId, actual);
    }

    [Fact]
    public void Resolve_ShouldReturnNull_WhenNoBody()
    {
        // Arrange
        var context = new DefaultHttpContext();
        var contextAccessor = new HttpContextAccessor()
        {
            HttpContext = context,
        };

        var resolver = new ResolveByBody(contextAccessor);

        // Act
        var value = resolver.Resolve();

        // Assert
        Assert.Null(value);
    }

    [Fact]
    public void Resolve_ShouldReturnOrganizationId_WhenBodyIsGuid()
    {
        // Arrange
        var organizationId = Guid.NewGuid();

        var context = new DefaultHttpContext();
        var contextAccessor = new HttpContextAccessor()
        {
            HttpContext = context,
        };

        using var stream = new MemoryStream();
        JsonSerializer.Serialize(stream, organizationId.ToString());
        context.Request.Body = stream;
        context.Request.ContentLength = stream.Length;

        var resolver = new ResolveByBody(contextAccessor);

        // Act
        var actual = resolver.Resolve();

        // Assert
        Assert.Equal(organizationId, actual);
    }


    [Fact]
    public void Resolve_ShouldReturnNull_WhenBodyIsNotGuid()
    {
        // Arrange
        var context = new DefaultHttpContext();
        var contextAccessor = new HttpContextAccessor()
        {
            HttpContext = context,
        };

        using var stream = new MemoryStream();
        JsonSerializer.Serialize(stream, "BAD_GUID");
        context.Request.Body = stream;
        context.Request.ContentLength = stream.Length;

        var resolver = new ResolveByBody(contextAccessor);

        // Act
        var actual = resolver.Resolve();

        // Assert
        Assert.Null(actual);
    }
}