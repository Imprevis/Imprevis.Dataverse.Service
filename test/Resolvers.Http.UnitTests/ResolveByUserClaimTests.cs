namespace Imprevis.Dataverse.Service.Resolvers.Http.UnitTests;

using Imprevis.Dataverse.Service.Resolvers.Http;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

public class ResolveByUserClaimTests
{
    [Fact]
    public void Resolve_ShouldReturnNull_WhenHttpContextAccessorIsNull()
    {
        // Arrange
        var resolver = new ResolveByUserClaim(null, "CLAIM_TYPE");

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
        var resolver = new ResolveByUserClaim(contextAccessor, "CLAIM_TYPE");

        // Act
        var value = resolver.Resolve();

        // Assert
        Assert.Null(value);
    }

    [Fact]
    public void Resolve_ShouldReturnNull_WhenRequestDoesNotContainClaim()
    {
        // Arrange
        var context = new DefaultHttpContext();
        var contextAccessor = new HttpContextAccessor()
        {
            HttpContext = context,
        };

        var resolver = new ResolveByUserClaim(contextAccessor, "CLAIM_TYPE");

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
        var claims = new List<Claim>()
        {
            new("CLAIM_TYPE", "BAD_GUID"),
        };
        var identity = new ClaimsIdentity(claims);
        var claimsPrincipal = new ClaimsPrincipal(identity);

        context.User.AddIdentity(identity);

        var resolver = new ResolveByUserClaim(contextAccessor, "CLAIM_TYPE");

        // Act
        var actual = resolver.Resolve();

        // Assert
        Assert.Null(actual);
    }

    [Fact]
    public void Resolve_ShouldReturnOrganizationId_WhenSuccessfullyParsesClaim()
    {
        // Arrange
        var organizationId = Guid.NewGuid();

        var context = new DefaultHttpContext();
        var contextAccessor = new HttpContextAccessor()
        {
            HttpContext = context,
        };
        var claims = new List<Claim>()
        {
            new("CLAIM_TYPE", organizationId.ToString()),
        };
        var identity = new ClaimsIdentity(claims);
        var claimsPrincipal = new ClaimsPrincipal(identity);

        context.User.AddIdentity(identity);

        var resolver = new ResolveByUserClaim(contextAccessor, "CLAIM_TYPE");

        // Act
        var actual = resolver.Resolve();

        // Assert
        Assert.Equal(organizationId, actual);
    }

    [Fact]
    public void Resolve_ShouldReturnOrganizationId_WhenSuccessfullyParsesClaimWithCustomParser()
    {
        // Arrange
        var organizationId = Guid.NewGuid();

        var context = new DefaultHttpContext();
        var contextAccessor = new HttpContextAccessor()
        {
            HttpContext = context,
        };
        var claims = new List<Claim>()
        {
            new("CLAIM_TYPE", $"--{organizationId}--"),
        };
        var identity = new ClaimsIdentity(claims);
        var claimsPrincipal = new ClaimsPrincipal(identity);

        context.User.AddIdentity(identity);

        var resolver = new ResolveByUserClaim(contextAccessor, "CLAIM_TYPE", value =>
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