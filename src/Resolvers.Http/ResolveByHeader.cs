namespace Imprevis.Dataverse.Service.Resolvers.Http;

using Imprevis.Dataverse.Service.Abstractions;
using Microsoft.AspNetCore.Http;

public class ResolveByHeader(IHttpContextAccessor? httpContextAccessor, string name, Func<string?, Guid?>? parse = null) : IDataverseServiceResolver
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

        var success = httpContext.Request.Headers.TryGetValue(name, out var values);
        if (success)
        {
            var value = values.FirstOrDefault();

            // Use custom processing method
            if (parse != null)
            {
                return parse(value);
            }

            // Parse as a Guid
            var parsed = Guid.TryParse(value, out var organizationId);
            if (parsed)
            {
                return organizationId;
            }
        }

        return null;
    }
}
