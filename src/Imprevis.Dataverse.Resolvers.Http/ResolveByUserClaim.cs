namespace Imprevis.Dataverse.Resolvers.Http;

using Imprevis.Dataverse.Abstractions;
using Microsoft.AspNetCore.Http;

public class ResolveByUserClaim(IHttpContextAccessor? httpContextAccessor, string type, Func<string?, Guid?>? parse = null) : IDataverseServiceResolver
{
    public Guid? Resolve()
    {
        if (httpContextAccessor == null)
        {
            return null;
        }

        var httpContext = httpContextAccessor.HttpContext;
        if (httpContext == null)
        {
            return null;
        }

        var claim = httpContext.User.Claims.FirstOrDefault(x => x.Type == type);
        if (claim == null)
        {
            return null;
        }

        // Use custom processing method
        if (parse != null)
        {
            return parse(claim.Value);
        }

        // Parse as a Guid
        var parsed = Guid.TryParse(claim.Value, out var organizationId);
        if (parsed)
        {
            return organizationId;
        }

        return null;
    }
}
